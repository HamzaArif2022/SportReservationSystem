using System.Collections.Generic;
using System.Windows.Forms;
using MetroFramework.Forms;
using SportReservationSystem.ClientApp;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormHistorique : MetroForm
    {
        private readonly ApiClient _apiClient;
        private readonly int _clientId;

        public FormHistorique(ApiClient apiClient, int clientId)
        {
            _apiClient = apiClient;
            _clientId = clientId;
            InitializeComponent();
            Load += async (_, _) => await LoadHistoriqueAsync();
            
            btnAnnuler.Click += async (_, _) => await AnnulerReservationAsync();
        }

        private async Task LoadHistoriqueAsync()
        {
            var reservations = await _apiClient.GetAsync<List<ReservationDto>>($"api/reservations/mes-reservations");
            dataGridViewHistorique.DataSource = reservations;
            
            // Colorer les lignes selon le statut
            foreach (DataGridViewRow row in dataGridViewHistorique.Rows)
            {
                var statut = row.Cells["Statut"].Value?.ToString();
                if (statut == "Confirmée")
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                else if (statut == "En attente")
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                else if (statut == "Annulée" || statut == "Refusée")
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
            }
        }

        private async Task AnnulerReservationAsync()
        {
            if (dataGridViewHistorique.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show("Sélectionnez une réservation à annuler.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (reservation.Statut != "Confirmée" && reservation.Statut != "En attente")
            {
                MessageBox.Show("Seules les réservations confirmées ou en attente peuvent être annulées.", 
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var result = MessageBox.Show($"Voulez-vous annuler la réservation du {reservation.DateCreneau:dd/MM/yyyy} ?", 
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                var success = await _apiClient.DeleteAsync($"api/reservations/{reservation.Id}");
                
                if (success)
                {
                    MessageBox.Show("Réservation annulée avec succès. Un remboursement sera effectué.", 
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadHistoriqueAsync();
                }
                else
                {
                    MessageBox.Show("Impossible d'annuler la réservation (délai dépassé).", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InitializeComponent()
        {
            this.dataGridViewHistorique = new System.Windows.Forms.DataGridView();
            this.btnAnnuler = new MetroFramework.Controls.MetroButton();
            this.lblTitle = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistorique)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewHistorique
            // 
            this.dataGridViewHistorique.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistorique.Location = new System.Drawing.Point(23, 60);
            this.dataGridViewHistorique.Name = "dataGridViewHistorique";
            this.dataGridViewHistorique.RowHeadersWidth = 51;
            this.dataGridViewHistorique.RowTemplate.Height = 24;
            this.dataGridViewHistorique.Size = new System.Drawing.Size(754, 320);
            this.dataGridViewHistorique.TabIndex = 0;
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Location = new System.Drawing.Point(657, 390);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(120, 35);
            this.btnAnnuler.TabIndex = 1;
            this.btnAnnuler.Text = "Annuler";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTitle.Location = new System.Drawing.Point(23, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(224, 25);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Historique des Réservations";
            // 
            // FormHistorique
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.dataGridViewHistorique);
            this.Text = "Mon Historique";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistorique)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dataGridViewHistorique;
        private MetroFramework.Controls.MetroButton btnAnnuler;
        private MetroFramework.Controls.MetroLabel lblTitle;
    }
}