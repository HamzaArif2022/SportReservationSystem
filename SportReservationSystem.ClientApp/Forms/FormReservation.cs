using System.Windows.Forms;
using MetroFramework.Forms;
using SportReservationSystem.ClientApp;
using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormReservation : MetroForm
    {
        private readonly ApiClient _apiClient;
        private readonly int _clientId;
        private Creneau _selectedCreneau;

        public FormReservation(ApiClient apiClient, int clientId, Creneau creneau)
        {
            _apiClient = apiClient;
            _clientId = clientId;
            _selectedCreneau = creneau;
            InitializeComponent();
            
            txtTerrain.Text = creneau.Terrain?.Nom ?? "Non spécifié";
            txtDate.Text = creneau.Date.ToString("dd/MM/yyyy");
            txtHeure.Text = $"{creneau.HeureDebut:hh\\:mm} - {creneau.HeureFin:hh\\:mm}";
            txtTarif.Text = $"{creneau.Terrain?.TarifHoraire} DH";
            txtTotal.Text = $"{creneau.Terrain?.TarifHoraire} DH";
            
            btnConfirmer.Click += async (_, _) => await ConfirmerAsync();
            btnAnnuler.Click += (_, _) => Close();
        }

        private async Task ConfirmerAsync()
        {
            var dto = new CreateReservationDto
            {
                CreneauId = _selectedCreneau.Id,
                MontantTotal = _selectedCreneau.Terrain?.TarifHoraire ?? 0
            };
    
            // CORRECTION : Utiliser PostAsync<T> avec un seul type générique
            var reservation = await _apiClient.PostAsync<ReservationDto>("api/reservations", dto);
    
            if (reservation != null)
            {
                MessageBox.Show("Réservation créée avec succès! Veuillez procéder au paiement.", "Succès", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        
                var paiementForm = new FormPaiement(_apiClient, _clientId, reservation.Id, reservation.MontantTotal);
                paiementForm.ShowDialog();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Erreur lors de la création de la réservation.", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeComponent()
        {
            this.lblTerrain = new MetroFramework.Controls.MetroLabel();
            this.txtTerrain = new MetroFramework.Controls.MetroTextBox();
            this.lblDate = new MetroFramework.Controls.MetroLabel();
            this.txtDate = new MetroFramework.Controls.MetroTextBox();
            this.lblHeure = new MetroFramework.Controls.MetroLabel();
            this.txtHeure = new MetroFramework.Controls.MetroTextBox();
            this.lblTarif = new MetroFramework.Controls.MetroLabel();
            this.txtTarif = new MetroFramework.Controls.MetroTextBox();
            this.lblTotal = new MetroFramework.Controls.MetroLabel();
            this.txtTotal = new MetroFramework.Controls.MetroTextBox();
            this.btnConfirmer = new MetroFramework.Controls.MetroButton();
            this.btnAnnuler = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // lblTerrain
            // 
            this.lblTerrain.AutoSize = true;
            this.lblTerrain.Location = new System.Drawing.Point(30, 60);
            this.lblTerrain.Name = "lblTerrain";
            this.lblTerrain.Size = new System.Drawing.Size(55, 20);
            this.lblTerrain.TabIndex = 0;
            this.lblTerrain.Text = "Terrain";
            // 
            // txtTerrain
            // 
            this.txtTerrain.Location = new System.Drawing.Point(150, 60);
            this.txtTerrain.Name = "txtTerrain";
            this.txtTerrain.ReadOnly = true;
            this.txtTerrain.Size = new System.Drawing.Size(250, 23);
            this.txtTerrain.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(30, 100);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 20);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(150, 100);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(250, 23);
            this.txtDate.TabIndex = 3;
            // 
            // lblHeure
            // 
            this.lblHeure.AutoSize = true;
            this.lblHeure.Location = new System.Drawing.Point(30, 140);
            this.lblHeure.Name = "lblHeure";
            this.lblHeure.Size = new System.Drawing.Size(48, 20);
            this.lblHeure.TabIndex = 4;
            this.lblHeure.Text = "Heure";
            // 
            // txtHeure
            // 
            this.txtHeure.Location = new System.Drawing.Point(150, 140);
            this.txtHeure.Name = "txtHeure";
            this.txtHeure.ReadOnly = true;
            this.txtHeure.Size = new System.Drawing.Size(250, 23);
            this.txtHeure.TabIndex = 5;
            // 
            // lblTarif
            // 
            this.lblTarif.AutoSize = true;
            this.lblTarif.Location = new System.Drawing.Point(30, 180);
            this.lblTarif.Name = "lblTarif";
            this.lblTarif.Size = new System.Drawing.Size(39, 20);
            this.lblTarif.TabIndex = 6;
            this.lblTarif.Text = "Tarif";
            // 
            // txtTarif
            // 
            this.txtTarif.Location = new System.Drawing.Point(150, 180);
            this.txtTarif.Name = "txtTarif";
            this.txtTarif.ReadOnly = true;
            this.txtTarif.Size = new System.Drawing.Size(250, 23);
            this.txtTarif.TabIndex = 7;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTotal.Location = new System.Drawing.Point(30, 230);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(49, 25);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txtTotal.Location = new System.Drawing.Point(150, 230);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(250, 29);
            this.txtTotal.TabIndex = 9;
            // 
            // btnConfirmer
            // 
            this.btnConfirmer.Location = new System.Drawing.Point(150, 300);
            this.btnConfirmer.Name = "btnConfirmer";
            this.btnConfirmer.Size = new System.Drawing.Size(120, 35);
            this.btnConfirmer.TabIndex = 10;
            this.btnConfirmer.Text = "Confirmer";
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Location = new System.Drawing.Point(280, 300);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(120, 35);
            this.btnAnnuler.TabIndex = 11;
            this.btnAnnuler.Text = "Annuler";
            // 
            // FormReservation
            // 
            this.ClientSize = new System.Drawing.Size(450, 380);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnConfirmer);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtTarif);
            this.Controls.Add(this.lblTarif);
            this.Controls.Add(this.txtHeure);
            this.Controls.Add(this.lblHeure);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.txtTerrain);
            this.Controls.Add(this.lblTerrain);
            this.Text = "Confirmer la Réservation";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private MetroFramework.Controls.MetroLabel lblTerrain;
        private MetroFramework.Controls.MetroTextBox txtTerrain;
        private MetroFramework.Controls.MetroLabel lblDate;
        private MetroFramework.Controls.MetroTextBox txtDate;
        private MetroFramework.Controls.MetroLabel lblHeure;
        private MetroFramework.Controls.MetroTextBox txtHeure;
        private MetroFramework.Controls.MetroLabel lblTarif;
        private MetroFramework.Controls.MetroTextBox txtTarif;
        private MetroFramework.Controls.MetroLabel lblTotal;
        private MetroFramework.Controls.MetroTextBox txtTotal;
        private MetroFramework.Controls.MetroButton btnConfirmer;
        private MetroFramework.Controls.MetroButton btnAnnuler;
    }
}