using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormHistorique : MetroFramework.Forms.MetroForm
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
            try
            {
                var reservations = await _apiClient.GetAsync<List<ReservationDto>>(
                    "api/reservations/mes-reservations");

                dataGridViewHistorique.DataSource = reservations;
                lblStatus.Text = $"{reservations?.Count ?? 0} réservation(s)";

                foreach (DataGridViewRow row in dataGridViewHistorique.Rows)
                {
                    if (row.DataBoundItem is ReservationDto r)
                    {
                        row.DefaultCellStyle.BackColor = r.Statut switch
                        {
                            "Confirmée" => Color.FromArgb(20, 80, 40),
                            "En attente" => Color.FromArgb(60, 50, 10),
                            "Annulée" or "Refusée" => Color.FromArgb(70, 15, 15),
                            _ => Color.FromArgb(0, 42, 26),
                        };
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(230, 255, 247);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur chargement historique :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AnnulerReservationAsync()
        {
            if (dataGridViewHistorique.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show(this,
                    "Sélectionnez une réservation à annuler.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (reservation.Statut != "Confirmée" && reservation.Statut != "En attente")
            {
                MessageBox.Show(this,
                    "Seules les réservations confirmées ou en attente peuvent être annulées.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(this,
                $"Annuler la réservation du {reservation.DateCreneau:dd/MM/yyyy} ?\n\n" +
                $"Terrain : {reservation.TerrainNom}\n" +
                $"Horaire : {reservation.HeureDebut:hh\\:mm} – {reservation.HeureFin:hh\\:mm}",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                btnAnnuler.Enabled = false;
                btnAnnuler.Text = "Annulation...";

                var ok = await _apiClient.DeleteAsync($"api/reservations/{reservation.Id}");
                if (ok)
                {
                    MessageBox.Show(this,
                        "Réservation annulée. Un remboursement sera effectué.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadHistoriqueAsync();
                }
                else
                {
                    MessageBox.Show(this,
                        "Impossible d'annuler (délai dépassé).",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                btnAnnuler.Enabled = true;
                btnAnnuler.Text = "✖  Annuler la réservation";
            }
        }
    }
}