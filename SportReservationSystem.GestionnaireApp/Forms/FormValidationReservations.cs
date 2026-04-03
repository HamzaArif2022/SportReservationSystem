using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.GestionnaireApp.Forms
{
    public partial class FormValidationReservations : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient;
        private List<ReservationDto> _reservations = new();

        public FormValidationReservations(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();

            Load += async (_, _) => await LoadReservationsAsync();
            btnValider.Click += async (_, _) => await ValiderReservationAsync();
            btnRefuser.Click += async (_, _) => await RefuserReservationAsync();
            btnRafraichir.Click += async (_, _) => await LoadReservationsAsync();
        }

        // ════════════════════════════════════════════════════════════════
        // CHARGEMENT
        // ════════════════════════════════════════════════════════════════
        private async Task LoadReservationsAsync()
        {
            try
            {
                _reservations = await _apiClient.GetAsync<List<ReservationDto>>(
                    "api/reservations/en-attente") ?? new();

                dataGridViewReservations.DataSource = _reservations;
                lblCount.Text = $"{_reservations.Count} réservation(s) en attente";

                bool hasRows = _reservations.Count > 0;
                btnValider.Enabled = hasRows;
                btnRefuser.Enabled = hasRows;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur chargement :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ════════════════════════════════════════════════════════════════
        // VALIDER
        // ════════════════════════════════════════════════════════════════
        private async Task ValiderReservationAsync()
        {
            if (dataGridViewReservations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
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
                    await LoadReservationsAsync();
                }
                else
                {
                    MessageBox.Show(this,
                        "Validation impossible.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally { SetValidateState(loading: false); }
        }

        // ════════════════════════════════════════════════════════════════
        // REFUSER
        // ════════════════════════════════════════════════════════════════
        private async Task RefuserReservationAsync()
        {
            if (dataGridViewReservations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show(this,
                    "Sélectionnez une réservation à refuser.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var motif = Microsoft.VisualBasic.Interaction.InputBox(
                "Motif du refus :", "Refus de réservation", "");

            if (string.IsNullOrWhiteSpace(motif))
            {
                MessageBox.Show(this,
                    "Un motif est requis pour refuser la réservation.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SetRefuseState(loading: true);
            try
            {
                var ok = await _apiClient.PutAsync(
                    $"api/reservations/{reservation.Id}/refuse", new { motif });
                if (ok)
                {
                    MessageBox.Show(this,
                        $"Réservation N°{reservation.Id} refusée.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadReservationsAsync();
                }
                else
                {
                    MessageBox.Show(this,
                        "Refus impossible.", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally { SetRefuseState(loading: false); }
        }

        // ════════════════════════════════════════════════════════════════
        // HELPERS
        // ════════════════════════════════════════════════════════════════
        private void SetValidateState(bool loading)
        {
            btnValider.Enabled = !loading;
            btnValider.Text = loading ? "Validation..." : "✔  Valider";
        }

        private void SetRefuseState(bool loading)
        {
            btnRefuser.Enabled = !loading;
            btnRefuser.Text = loading ? "Refus..." : "✖  Refuser";
        }
    }
}