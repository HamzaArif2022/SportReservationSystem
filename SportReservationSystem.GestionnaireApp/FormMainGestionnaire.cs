using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SportReservationSystem.GestionnaireApp.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.GestionnaireApp
{
    public partial class FormMainGestionnaire : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient;

        public FormMainGestionnaire(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();

            // Navigation rapide
            _btnValidation.Click += (_, _) => new FormValidationReservations(_apiClient).ShowDialog();
            _btnPlanning.Click += (_, _) => new FormPlanning(_apiClient).ShowDialog();
            _btnStatistiques.Click += (_, _) => new FormStatistiques(_apiClient).ShowDialog();

            // Grille Dashboard
            _btnRefreshDashboard.Click += async (_, _) => await LoadDashboardAsync();

            // Grille Validations
            _btnRefresh.Click += async (_, _) => await LoadValidationsAsync();
            _btnValidate.Click += async (_, _) => await ValidateSelectedAsync();
            _btnCancel.Click += async (_, _) => await CancelSelectedAsync();

            Load += async (_, _) =>
            {
                await LoadDashboardAsync();
                await LoadValidationsAsync();
            };
        }

        // ════════════════════════════════════════════════════════════════
        // CHARGEMENT DASHBOARD
        // ════════════════════════════════════════════════════════════════
        private async Task LoadDashboardAsync()
        {
            try
            {
                var data = await _apiClient.GetAsync<List<ReservationDto>>(
                    "api/reservations/planning?date=" + DateTime.Today.ToString("yyyy-MM-dd"));

                _gridDashboard.DataSource = data;

                foreach (DataGridViewRow row in _gridDashboard.Rows)
                {
                    if (row.DataBoundItem is ReservationDto r)
                    {
                        row.DefaultCellStyle.BackColor = r.Statut switch
                        {
                            "Confirmée" => Color.FromArgb(20, 80, 40),
                            "En attente" => Color.FromArgb(60, 50, 10),
                            "Annulée" => Color.FromArgb(70, 15, 15),
                            _ => Color.FromArgb(18, 35, 65),
                        };
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(225, 235, 255);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur chargement planning :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ════════════════════════════════════════════════════════════════
        // CHARGEMENT VALIDATIONS
        // ════════════════════════════════════════════════════════════════
        private async Task LoadValidationsAsync()
        {
            try
            {
                var data = await _apiClient.GetAsync<List<ReservationDto>>("api/reservations/en-attente");
                _gridValidations.DataSource = data;

                bool hasRows = data != null && data.Count > 0;
                _btnValidate.Enabled = hasRows;
                _btnCancel.Enabled = hasRows;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur chargement validations :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ════════════════════════════════════════════════════════════════
        // VALIDER
        // ════════════════════════════════════════════════════════════════
        private async Task ValidateSelectedAsync()
        {
            if (_gridValidations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show(this,
                    "Sélectionnez une réservation à valider.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(this,
                $"Valider la réservation de {reservation.ClientNom} {reservation.ClientPrenom} ?\n\n" +
                $"Terrain : {reservation.TerrainNom}\n" +
                $"Date    : {reservation.DateCreneau:dd/MM/yyyy}\n" +
                $"Horaire : {reservation.HeureDebut:hh\\:mm} – {reservation.HeureFin:hh\\:mm}\n" +
                $"Montant : {reservation.MontantTotal} DH",
                "Confirmer la validation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            SetValidateState(loading: true);
            try
            {
                var ok = await _apiClient.PutAsync($"api/reservations/{reservation.Id}/validate");
                if (ok)
                {
                    MessageBox.Show(this,
                        $"Réservation N°{reservation.Id} validée avec succès.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadValidationsAsync();
                    await LoadDashboardAsync();
                }
                else
                {
                    MessageBox.Show(this,
                        "Validation impossible.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally { SetValidateState(loading: false); }
        }

        // ════════════════════════════════════════════════════════════════
        // ANNULER
        // ════════════════════════════════════════════════════════════════
        private async Task CancelSelectedAsync()
        {
            if (_gridValidations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show(this,
                    "Sélectionnez une réservation à annuler.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(this,
                $"Annuler la réservation de {reservation.ClientNom} {reservation.ClientPrenom} ?\n\n" +
                $"Terrain : {reservation.TerrainNom}\n" +
                $"Date    : {reservation.DateCreneau:dd/MM/yyyy}\n" +
                $"Horaire : {reservation.HeureDebut:hh\\:mm} – {reservation.HeureFin:hh\\:mm}\n" +
                $"Montant : {reservation.MontantTotal} DH",
                "Confirmer l'annulation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            SetCancelState(loading: true);
            try
            {
                var ok = await _apiClient.PutAsync($"api/reservations/{reservation.Id}/cancel");
                if (ok)
                {
                    MessageBox.Show(this,
                        $"Réservation N°{reservation.Id} annulée.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadValidationsAsync();
                    await LoadDashboardAsync();
                }
                else
                {
                    MessageBox.Show(this,
                        "Annulation impossible.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally { SetCancelState(loading: false); }
        }

        // ════════════════════════════════════════════════════════════════
        // HELPERS
        // ════════════════════════════════════════════════════════════════
        private void SetValidateState(bool loading)
        {
            _btnValidate.Enabled = !loading;
            _btnValidate.Text = loading ? "Validation..." : "✔ Valider";
        }

        private void SetCancelState(bool loading)
        {
            _btnCancel.Enabled = !loading;
            _btnCancel.Text = loading ? "Annulation..." : "✖ Annuler";
        }
    }
}