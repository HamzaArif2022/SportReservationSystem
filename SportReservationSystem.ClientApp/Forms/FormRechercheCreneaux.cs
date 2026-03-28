using MetroFramework.Forms;
using SportReservationSystem.ClientApp;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormRechercheCreneaux : MetroForm
    {
        private readonly ApiClient _apiClient;
        private List<Terrain> _terrains;
        private List<Creneau> _creneaux;

        public FormRechercheCreneaux(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            Load += async (_, _) => await LoadTerrainsAsync();
            btnRechercher.Click += async (_, _) => await RechercherAsync();
        }

        private async Task LoadTerrainsAsync()
        {
            _terrains = await _apiClient.GetAsync<List<Terrain>>("api/terrains");
            cmbTerrain.DataSource = _terrains;
            cmbTerrain.DisplayMember = "Nom";
            cmbTerrain.ValueMember = "Id";
        }

        private async Task RechercherAsync()
        {
            int terrainId = (int)cmbTerrain.SelectedValue;
            DateTime date = dtpDate.Value.Date;
            
            _creneaux = await _apiClient.GetAsync<List<Creneau>>($"api/creneaux/disponibles?terrainId={terrainId}&date={date:yyyy-MM-dd}");
            dataGridViewCreneaux.DataSource = _creneaux;
            
            if (_creneaux.Count == 0)
                lblStatus.Text = "Aucun créneau disponible pour cette date";
            else
                lblStatus.Text = $"{_creneaux.Count} créneaux disponibles";
        }

        private void InitializeComponent()
        {
            this.cmbTerrain = new MetroFramework.Controls.MetroComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.btnRechercher = new MetroFramework.Controls.MetroButton();
            this.dataGridViewCreneaux = new System.Windows.Forms.DataGridView();
            this.lblTerrain = new MetroFramework.Controls.MetroLabel();
            this.lblDate = new MetroFramework.Controls.MetroLabel();
            this.lblStatus = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCreneaux)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbTerrain
            // 
            this.cmbTerrain.FormattingEnabled = true;
            this.cmbTerrain.Location = new System.Drawing.Point(100, 60);
            this.cmbTerrain.Name = "cmbTerrain";
            this.cmbTerrain.Size = new System.Drawing.Size(250, 30);
            this.cmbTerrain.TabIndex = 0;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(100, 110);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(250, 22);
            this.dtpDate.TabIndex = 1;
            // 
            // btnRechercher
            // 
            this.btnRechercher.Location = new System.Drawing.Point(380, 100);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(120, 30);
            this.btnRechercher.TabIndex = 2;
            this.btnRechercher.Text = "Rechercher";
            // 
            // dataGridViewCreneaux
            // 
            this.dataGridViewCreneaux.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCreneaux.Location = new System.Drawing.Point(23, 160);
            this.dataGridViewCreneaux.Name = "dataGridViewCreneaux";
            this.dataGridViewCreneaux.RowHeadersWidth = 51;
            this.dataGridViewCreneaux.RowTemplate.Height = 24;
            this.dataGridViewCreneaux.Size = new System.Drawing.Size(754, 200);
            this.dataGridViewCreneaux.TabIndex = 3;
            // 
            // lblTerrain
            // 
            this.lblTerrain.AutoSize = true;
            this.lblTerrain.Location = new System.Drawing.Point(23, 65);
            this.lblTerrain.Name = "lblTerrain";
            this.lblTerrain.Size = new System.Drawing.Size(55, 20);
            this.lblTerrain.TabIndex = 4;
            this.lblTerrain.Text = "Terrain";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(23, 110);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 20);
            this.lblDate.TabIndex = 5;
            this.lblDate.Text = "Date";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(23, 370);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 20);
            this.lblStatus.TabIndex = 6;
            // 
            // FormRechercheCreneaux
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTerrain);
            this.Controls.Add(this.dataGridViewCreneaux);
            this.Controls.Add(this.btnRechercher);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.cmbTerrain);
            this.Text = "Rechercher un Créneau";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCreneaux)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private MetroFramework.Controls.MetroComboBox cmbTerrain;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private MetroFramework.Controls.MetroButton btnRechercher;
        private System.Windows.Forms.DataGridView dataGridViewCreneaux;
        private MetroFramework.Controls.MetroLabel lblTerrain;
        private MetroFramework.Controls.MetroLabel lblDate;
        private MetroFramework.Controls.MetroLabel lblStatus;
    }
}