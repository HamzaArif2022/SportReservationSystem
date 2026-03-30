using MetroFramework.Forms;
using SportReservationSystem.GestionnaireApp;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.GestionnaireApp.Forms
{
    public partial class FormStatistiques : MetroForm
    {
        private readonly ApiClient _apiClient;

        public FormStatistiques(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            Load += async (_, _) => await LoadDashboardAsync();
            
            cmbPeriode.SelectedIndex = 0;
            cmbPeriode.SelectedIndexChanged += async (_, _) => await LoadDashboardAsync();
            btnExporter.Click += async (_, _) => await ExporterRapportAsync();
        }

        private async Task LoadDashboardAsync()
        {
            var dashboard = await _apiClient.GetAsync<DashboardStatsDto>("api/statistiques/dashboard");
            
            if (dashboard != null)
            {
                lblAujourdHui.Text = dashboard.NombreReservationsAujourdHui.ToString();
                lblEnAttente.Text = dashboard.NombreReservationsEnAttente.ToString();
                lblConfirmees.Text = dashboard.NombreReservationsConfirmées.ToString();
                lblCAMois.Text = $"{dashboard.ChiffreAffairesMois} DH";
                lblOccupation.Text = $"{dashboard.TauxOccupationMois}%";
                
                dataGridViewDernieres.DataSource = dashboard.DernieresReservations;
                dataGridViewTopTerrains.DataSource = dashboard.TopTerrains;
            }
            
            await LoadChiffreAffairesAsync();
        }

        private async Task LoadChiffreAffairesAsync()
        {
            var periode = cmbPeriode.SelectedItem?.ToString() ?? "Mois";
            DateTime debut, fin;
            
            if (periode == "Semaine")
            {
                debut = DateTime.Today.AddDays(-7);
                fin = DateTime.Today;
            }
            else if (periode == "Mois")
            {
                debut = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                fin = DateTime.Today;
            }
            else // Année
            {
                debut = new DateTime(DateTime.Today.Year, 1, 1);
                fin = DateTime.Today;
            }
            
            var ca = await _apiClient.GetAsync<ChiffreAffairesDto>($"api/statistiques/chiffre-affaires?debut={debut:yyyy-MM-dd}&fin={fin:yyyy-MM-dd}");
            
            if (ca != null)
            {
                lblCATotal.Text = $"{ca.Total} DH";
                lblCAEvolution.Text = $"{ca.Evolution}%";
                
                dataGridViewCAMois.DataSource = ca.ParMois.Select(kv => new { Mois = kv.Key, Montant = $"{kv.Value} DH" }).ToList();
                dataGridViewCATerrain.DataSource = ca.ParTerrain.Select(kv => new { Terrain = kv.Key, Montant = $"{kv.Value} DH" }).ToList();
            }
        }

        private async Task ExporterRapportAsync()
        {
            var debut = DateTime.Today.AddMonths(-1);
            var fin = DateTime.Today;
            
            var rapport = await _apiClient.GetFileAsync($"api/statistiques/export?debut={debut:yyyy-MM-dd}&fin={fin:yyyy-MM-dd}");
            
            if (rapport != null)
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Fichier texte (*.txt)|*.txt",
                    FileName = $"rapport_{debut:yyyyMMdd}_{fin:yyyyMMdd}.txt"
                };
                
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    await System.IO.File.WriteAllBytesAsync(saveFileDialog.FileName, rapport);
                    MessageBox.Show("Rapport exporté avec succès.", "Succès", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void InitializeComponent()
        {
            this.tabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabDashboard = new MetroFramework.Controls.MetroTabPage();
            this.groupBoxStats = new System.Windows.Forms.GroupBox();
            this.lblOccupation = new MetroFramework.Controls.MetroLabel();
            this.lblOccupationTitle = new MetroFramework.Controls.MetroLabel();
            this.lblCAMois = new MetroFramework.Controls.MetroLabel();
            this.lblCAMoisTitle = new MetroFramework.Controls.MetroLabel();
            this.lblConfirmees = new MetroFramework.Controls.MetroLabel();
            this.lblConfirmeesTitle = new MetroFramework.Controls.MetroLabel();
            this.lblEnAttente = new MetroFramework.Controls.MetroLabel();
            this.lblEnAttenteTitle = new MetroFramework.Controls.MetroLabel();
            this.lblAujourdHui = new MetroFramework.Controls.MetroLabel();
            this.lblAujourdHuiTitle = new MetroFramework.Controls.MetroLabel();
            this.groupBoxDernieres = new System.Windows.Forms.GroupBox();
            this.dataGridViewDernieres = new System.Windows.Forms.DataGridView();
            this.groupBoxTopTerrains = new System.Windows.Forms.GroupBox();
            this.dataGridViewTopTerrains = new System.Windows.Forms.DataGridView();
            this.tabCA = new MetroFramework.Controls.MetroTabPage();
            this.groupBoxTotal = new System.Windows.Forms.GroupBox();
            this.lblCAEvolution = new MetroFramework.Controls.MetroLabel();
            this.lblCAEvolutionTitle = new MetroFramework.Controls.MetroLabel();
            this.lblCATotal = new MetroFramework.Controls.MetroLabel();
            this.lblCATotalTitle = new MetroFramework.Controls.MetroLabel();
            this.cmbPeriode = new MetroFramework.Controls.MetroComboBox();
            this.groupBoxParMois = new System.Windows.Forms.GroupBox();
            this.dataGridViewCAMois = new System.Windows.Forms.DataGridView();
            this.groupBoxParTerrain = new System.Windows.Forms.GroupBox();
            this.dataGridViewCATerrain = new System.Windows.Forms.DataGridView();
            this.btnExporter = new MetroFramework.Controls.MetroButton();
            this.tabControl.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.groupBoxStats.SuspendLayout();
            this.groupBoxDernieres.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDernieres)).BeginInit();
            this.groupBoxTopTerrains.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTopTerrains)).BeginInit();
            this.tabCA.SuspendLayout();
            this.groupBoxTotal.SuspendLayout();
            this.groupBoxParMois.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCAMois)).BeginInit();
            this.groupBoxParTerrain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCATerrain)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabDashboard);
            this.tabControl.Controls.Add(this.tabCA);
            this.tabControl.Location = new System.Drawing.Point(23, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.Size = new System.Drawing.Size(1054, 550);
            this.tabControl.TabIndex = 0;
            // 
            // tabDashboard
            // 
            this.tabDashboard.Controls.Add(this.groupBoxTopTerrains);
            this.tabDashboard.Controls.Add(this.groupBoxDernieres);
            this.tabDashboard.Controls.Add(this.groupBoxStats);
            this.tabDashboard.HorizontalScrollbarBarColor = true;
            this.tabDashboard.Location = new System.Drawing.Point(4, 38);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Size = new System.Drawing.Size(1046, 508);
            this.tabDashboard.TabIndex = 0;
            this.tabDashboard.Text = "Tableau de bord";
            // 
            // groupBoxStats
            // 
            this.groupBoxStats.Controls.Add(this.lblOccupation);
            this.groupBoxStats.Controls.Add(this.lblOccupationTitle);
            this.groupBoxStats.Controls.Add(this.lblCAMois);
            this.groupBoxStats.Controls.Add(this.lblCAMoisTitle);
            this.groupBoxStats.Controls.Add(this.lblConfirmees);
            this.groupBoxStats.Controls.Add(this.lblConfirmeesTitle);
            this.groupBoxStats.Controls.Add(this.lblEnAttente);
            this.groupBoxStats.Controls.Add(this.lblEnAttenteTitle);
            this.groupBoxStats.Controls.Add(this.lblAujourdHui);
            this.groupBoxStats.Controls.Add(this.lblAujourdHuiTitle);
            this.groupBoxStats.Location = new System.Drawing.Point(10, 10);
            this.groupBoxStats.Name = "groupBoxStats";
            this.groupBoxStats.Size = new System.Drawing.Size(1020, 120);
            this.groupBoxStats.TabIndex = 0;
            this.groupBoxStats.TabStop = false;
            this.groupBoxStats.Text = "Statistiques rapides";
            // 
            // lblOccupation
            // 
            this.lblOccupation.AutoSize = true;
            this.lblOccupation.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblOccupation.Location = new System.Drawing.Point(820, 50);
            this.lblOccupation.Name = "lblOccupation";
            this.lblOccupation.Size = new System.Drawing.Size(0, 25);
            this.lblOccupation.TabIndex = 9;
            // 
            // lblOccupationTitle
            // 
            this.lblOccupationTitle.AutoSize = true;
            this.lblOccupationTitle.Location = new System.Drawing.Point(820, 20);
            this.lblOccupationTitle.Name = "lblOccupationTitle";
            this.lblOccupationTitle.Size = new System.Drawing.Size(107, 20);
            this.lblOccupationTitle.TabIndex = 8;
            this.lblOccupationTitle.Text = "Taux occupation";
            // 
            // lblCAMois
            // 
            this.lblCAMois.AutoSize = true;
            this.lblCAMois.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblCAMois.Location = new System.Drawing.Point(620, 50);
            this.lblCAMois.Name = "lblCAMois";
            this.lblCAMois.Size = new System.Drawing.Size(0, 25);
            this.lblCAMois.TabIndex = 7;
            // 
            // lblCAMoisTitle
            // 
            this.lblCAMoisTitle.AutoSize = true;
            this.lblCAMoisTitle.Location = new System.Drawing.Point(620, 20);
            this.lblCAMoisTitle.Name = "lblCAMoisTitle";
            this.lblCAMoisTitle.Size = new System.Drawing.Size(94, 20);
            this.lblCAMoisTitle.TabIndex = 6;
            this.lblCAMoisTitle.Text = "CA du mois";
            // 
            // lblConfirmees
            // 
            this.lblConfirmees.AutoSize = true;
            this.lblConfirmees.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblConfirmees.Location = new System.Drawing.Point(420, 50);
            this.lblConfirmees.Name = "lblConfirmees";
            this.lblConfirmees.Size = new System.Drawing.Size(0, 25);
            this.lblConfirmees.TabIndex = 5;
            // 
            // lblConfirmeesTitle
            // 
            this.lblConfirmeesTitle.AutoSize = true;
            this.lblConfirmeesTitle.Location = new System.Drawing.Point(420, 20);
            this.lblConfirmeesTitle.Name = "lblConfirmeesTitle";
            this.lblConfirmeesTitle.Size = new System.Drawing.Size(88, 20);
            this.lblConfirmeesTitle.TabIndex = 4;
            this.lblConfirmeesTitle.Text = "Confirmées";
            // 
            // lblEnAttente
            // 
            this.lblEnAttente.AutoSize = true;
            this.lblEnAttente.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblEnAttente.Location = new System.Drawing.Point(220, 50);
            this.lblEnAttente.Name = "lblEnAttente";
            this.lblEnAttente.Size = new System.Drawing.Size(0, 25);
            this.lblEnAttente.TabIndex = 3;
            // 
            // lblEnAttenteTitle
            // 
            this.lblEnAttenteTitle.AutoSize = true;
            this.lblEnAttenteTitle.Location = new System.Drawing.Point(220, 20);
            this.lblEnAttenteTitle.Name = "lblEnAttenteTitle";
            this.lblEnAttenteTitle.Size = new System.Drawing.Size(76, 20);
            this.lblEnAttenteTitle.TabIndex = 2;
            this.lblEnAttenteTitle.Text = "En attente";
            // 
            // lblAujourdHui
            // 
            this.lblAujourdHui.AutoSize = true;
            this.lblAujourdHui.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblAujourdHui.Location = new System.Drawing.Point(20, 50);
            this.lblAujourdHui.Name = "lblAujourdHui";
            this.lblAujourdHui.Size = new System.Drawing.Size(0, 25);
            this.lblAujourdHui.TabIndex = 1;
            // 
            // lblAujourdHuiTitle
            // 
            this.lblAujourdHuiTitle.AutoSize = true;
            this.lblAujourdHuiTitle.Location = new System.Drawing.Point(20, 20);
            this.lblAujourdHuiTitle.Name = "lblAujourdHuiTitle";
            this.lblAujourdHuiTitle.Size = new System.Drawing.Size(85, 20);
            this.lblAujourdHuiTitle.TabIndex = 0;
            this.lblAujourdHuiTitle.Text = "Aujourd'hui";
            // 
            // groupBoxDernieres
            // 
            this.groupBoxDernieres.Controls.Add(this.dataGridViewDernieres);
            this.groupBoxDernieres.Location = new System.Drawing.Point(10, 140);
            this.groupBoxDernieres.Name = "groupBoxDernieres";
            this.groupBoxDernieres.Size = new System.Drawing.Size(500, 350);
            this.groupBoxDernieres.TabIndex = 1;
            this.groupBoxDernieres.TabStop = false;
            this.groupBoxDernieres.Text = "Dernières réservations";
            // 
            // dataGridViewDernieres
            // 
            this.dataGridViewDernieres.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDernieres.Location = new System.Drawing.Point(10, 20);
            this.dataGridViewDernieres.Name = "dataGridViewDernieres";
            this.dataGridViewDernieres.RowHeadersWidth = 51;
            this.dataGridViewDernieres.RowTemplate.Height = 24;
            this.dataGridViewDernieres.Size = new System.Drawing.Size(480, 320);
            this.dataGridViewDernieres.TabIndex = 0;
            // 
            // groupBoxTopTerrains
            // 
            this.groupBoxTopTerrains.Controls.Add(this.dataGridViewTopTerrains);
            this.groupBoxTopTerrains.Location = new System.Drawing.Point(530, 140);
            this.groupBoxTopTerrains.Name = "groupBoxTopTerrains";
            this.groupBoxTopTerrains.Size = new System.Drawing.Size(500, 350);
            this.groupBoxTopTerrains.TabIndex = 2;
            this.groupBoxTopTerrains.TabStop = false;
            this.groupBoxTopTerrains.Text = "Top 5 terrains";
            // 
            // dataGridViewTopTerrains
            // 
            this.dataGridViewTopTerrains.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTopTerrains.Location = new System.Drawing.Point(10, 20);
            this.dataGridViewTopTerrains.Name = "dataGridViewTopTerrains";
            this.dataGridViewTopTerrains.RowHeadersWidth = 51;
            this.dataGridViewTopTerrains.RowTemplate.Height = 24;
            this.dataGridViewTopTerrains.Size = new System.Drawing.Size(480, 320);
            this.dataGridViewTopTerrains.TabIndex = 0;
            // 
            // tabCA
            // 
            this.tabCA.Controls.Add(this.btnExporter);
            this.tabCA.Controls.Add(this.groupBoxParTerrain);
            this.tabCA.Controls.Add(this.groupBoxParMois);
            this.tabCA.Controls.Add(this.cmbPeriode);
            this.tabCA.Controls.Add(this.groupBoxTotal);
            this.tabCA.HorizontalScrollbarBarColor = true;
            this.tabCA.Location = new System.Drawing.Point(4, 38);
            this.tabCA.Name = "tabCA";
            this.tabCA.Size = new System.Drawing.Size(1046, 508);
            this.tabCA.TabIndex = 1;
            this.tabCA.Text = "Chiffre d'affaires";
            // 
            // groupBoxTotal
            // 
            this.groupBoxTotal.Controls.Add(this.lblCAEvolution);
            this.groupBoxTotal.Controls.Add(this.lblCAEvolutionTitle);
            this.groupBoxTotal.Controls.Add(this.lblCATotal);
            this.groupBoxTotal.Controls.Add(this.lblCATotalTitle);
            this.groupBoxTotal.Location = new System.Drawing.Point(10, 10);
            this.groupBoxTotal.Name = "groupBoxTotal";
            this.groupBoxTotal.Size = new System.Drawing.Size(1020, 80);
            this.groupBoxTotal.TabIndex = 0;
            this.groupBoxTotal.TabStop = false;
            this.groupBoxTotal.Text = "Total";
            // 
            // lblCAEvolution
            // 
            this.lblCAEvolution.AutoSize = true;
            this.lblCAEvolution.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblCAEvolution.Location = new System.Drawing.Point(620, 35);
            this.lblCAEvolution.Name = "lblCAEvolution";
            this.lblCAEvolution.Size = new System.Drawing.Size(0, 25);
            this.lblCAEvolution.TabIndex = 3;
            // 
            // lblCAEvolutionTitle
            // 
            this.lblCAEvolutionTitle.AutoSize = true;
            this.lblCAEvolutionTitle.Location = new System.Drawing.Point(620, 10);
            this.lblCAEvolutionTitle.Name = "lblCAEvolutionTitle";
            this.lblCAEvolutionTitle.Size = new System.Drawing.Size(72, 20);
            this.lblCAEvolutionTitle.TabIndex = 2;
            this.lblCAEvolutionTitle.Text = "Évolution";
            // 
            // lblCATotal
            // 
            this.lblCATotal.AutoSize = true;
            this.lblCATotal.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblCATotal.Location = new System.Drawing.Point(20, 35);
            this.lblCATotal.Name = "lblCATotal";
            this.lblCATotal.Size = new System.Drawing.Size(0, 25);
            this.lblCATotal.TabIndex = 1;
            // 
            // lblCATotalTitle
            // 
            this.lblCATotalTitle.AutoSize = true;
            this.lblCATotalTitle.Location = new System.Drawing.Point(20, 10);
            this.lblCATotalTitle.Name = "lblCATotalTitle";
            this.lblCATotalTitle.Size = new System.Drawing.Size(39, 20);
            this.lblCATotalTitle.TabIndex = 0;
            this.lblCATotalTitle.Text = "Total";
            // 
            // cmbPeriode
            // 
            this.cmbPeriode.FormattingEnabled = true;
            this.cmbPeriode.Items.AddRange(new object[] { "Semaine", "Mois", "Année" });
            this.cmbPeriode.Location = new System.Drawing.Point(900, 100);
            this.cmbPeriode.Name = "cmbPeriode";
            this.cmbPeriode.Size = new System.Drawing.Size(130, 30);
            this.cmbPeriode.TabIndex = 1;
            // 
            // groupBoxParMois
            // 
            this.groupBoxParMois.Controls.Add(this.dataGridViewCAMois);
            this.groupBoxParMois.Location = new System.Drawing.Point(10, 100);
            this.groupBoxParMois.Name = "groupBoxParMois";
            this.groupBoxParMois.Size = new System.Drawing.Size(500, 380);
            this.groupBoxParMois.TabIndex = 2;
            this.groupBoxParMois.TabStop = false;
            this.groupBoxParMois.Text = "Par mois";
            // 
            // dataGridViewCAMois
            // 
            this.dataGridViewCAMois.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCAMois.Location = new System.Drawing.Point(10, 20);
            this.dataGridViewCAMois.Name = "dataGridViewCAMois";
            this.dataGridViewCAMois.RowHeadersWidth = 51;
            this.dataGridViewCAMois.RowTemplate.Height = 24;
            this.dataGridViewCAMois.Size = new System.Drawing.Size(480, 350);
            this.dataGridViewCAMois.TabIndex = 0;
            // 
            // groupBoxParTerrain
            // 
            this.groupBoxParTerrain.Controls.Add(this.dataGridViewCATerrain);
            this.groupBoxParTerrain.Location = new System.Drawing.Point(530, 100);
            this.groupBoxParTerrain.Name = "groupBoxParTerrain";
            this.groupBoxParTerrain.Size = new System.Drawing.Size(500, 380);
            this.groupBoxParTerrain.TabIndex = 3;
            this.groupBoxParTerrain.TabStop = false;
            this.groupBoxParTerrain.Text = "Par terrain";
            // 
            // dataGridViewCATerrain
            // 
            this.dataGridViewCATerrain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCATerrain.Location = new System.Drawing.Point(10, 20);
            this.dataGridViewCATerrain.Name = "dataGridViewCATerrain";
            this.dataGridViewCATerrain.RowHeadersWidth = 51;
            this.dataGridViewCATerrain.RowTemplate.Height = 24;
            this.dataGridViewCATerrain.Size = new System.Drawing.Size(480, 350);
            this.dataGridViewCATerrain.TabIndex = 0;
            // 
            // btnExporter
            // 
            this.btnExporter.Location = new System.Drawing.Point(900, 490);
            this.btnExporter.Name = "btnExporter";
            this.btnExporter.Size = new System.Drawing.Size(130, 35);
            this.btnExporter.TabIndex = 4;
            this.btnExporter.Text = "Exporter rapport";
            // 
            // FormStatistiques
            // 
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.tabControl);
            this.Text = "Statistiques";
            this.tabControl.ResumeLayout(false);
            this.tabDashboard.ResumeLayout(false);
            this.groupBoxStats.ResumeLayout(false);
            this.groupBoxStats.PerformLayout();
            this.groupBoxDernieres.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDernieres)).EndInit();
            this.groupBoxTopTerrains.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTopTerrains)).EndInit();
            this.tabCA.ResumeLayout(false);
            this.groupBoxTotal.ResumeLayout(false);
            this.groupBoxTotal.PerformLayout();
            this.groupBoxParMois.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCAMois)).EndInit();
            this.groupBoxParTerrain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCATerrain)).EndInit();
            this.ResumeLayout(false);
        }

        private MetroFramework.Controls.MetroTabControl tabControl;
        private MetroFramework.Controls.MetroTabPage tabDashboard;
        private MetroFramework.Controls.MetroTabPage tabCA;
        private System.Windows.Forms.GroupBox groupBoxStats;
        private MetroFramework.Controls.MetroLabel lblAujourdHui;
        private MetroFramework.Controls.MetroLabel lblAujourdHuiTitle;
        private MetroFramework.Controls.MetroLabel lblCAMois;
        private MetroFramework.Controls.MetroLabel lblCAMoisTitle;
        private MetroFramework.Controls.MetroLabel lblConfirmees;
        private MetroFramework.Controls.MetroLabel lblConfirmeesTitle;
        private MetroFramework.Controls.MetroLabel lblEnAttente;
        private MetroFramework.Controls.MetroLabel lblEnAttenteTitle;
        private MetroFramework.Controls.MetroLabel lblOccupation;
        private MetroFramework.Controls.MetroLabel lblOccupationTitle;
        private System.Windows.Forms.GroupBox groupBoxDernieres;
        private System.Windows.Forms.DataGridView dataGridViewDernieres;
        private System.Windows.Forms.GroupBox groupBoxTopTerrains;
        private System.Windows.Forms.DataGridView dataGridViewTopTerrains;
        private System.Windows.Forms.GroupBox groupBoxTotal;
        private MetroFramework.Controls.MetroLabel lblCATotal;
        private MetroFramework.Controls.MetroLabel lblCATotalTitle;
        private MetroFramework.Controls.MetroLabel lblCAEvolution;
        private MetroFramework.Controls.MetroLabel lblCAEvolutionTitle;
        private MetroFramework.Controls.MetroComboBox cmbPeriode;
        private System.Windows.Forms.GroupBox groupBoxParMois;
        private System.Windows.Forms.DataGridView dataGridViewCAMois;
        private System.Windows.Forms.GroupBox groupBoxParTerrain;
        private System.Windows.Forms.DataGridView dataGridViewCATerrain;
        private MetroFramework.Controls.MetroButton btnExporter;
    }
}