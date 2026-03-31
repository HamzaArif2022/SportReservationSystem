using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SportReservationSystem.GestionnaireApp.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.GestionnaireApp
{
    public partial class FormMainGestionnaire : Form
    {
        private readonly ApiClient _apiClient;

        public FormMainGestionnaire(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            
            // Événements pour les boutons de gestion
            _btnRefreshDashboard.Click += async (_, _) => await LoadDashboardAsync();
            _btnRefresh.Click += async (_, _) => await LoadValidationsAsync();
            _btnValidate.Click += async (_, _) => await ValidateSelectedAsync();
            _btnCancel.Click += async (_, _) => await CancelSelectedAsync();
            
            // Événements pour les boutons de navigation
            _btnValidation.Click += (_, _) => new FormValidationReservations(_apiClient).ShowDialog();
            _btnPlanning.Click += (_, _) => new FormPlanning(_apiClient).ShowDialog();
            _btnStatistiques.Click += (_, _) => new FormStatistiques(_apiClient).ShowDialog();

            Load += async (_, _) =>
            {
                await LoadDashboardAsync();
                await LoadValidationsAsync();
            };
        }

        private async Task LoadDashboardAsync()
        {
            try
            {
                var data = await _apiClient.GetAsync<List<ReservationDto>>("api/reservations/planning?date=" + DateTime.Today.ToString("yyyy-MM-dd"));
                _gridDashboard.DataSource = data;
                
                // Colorer les lignes selon le statut
                foreach (DataGridViewRow row in _gridDashboard.Rows)
                {
                    if (row.DataBoundItem is ReservationDto reservation)
                    {
                        if (reservation.Statut == "Confirmée")
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                        else if (reservation.Statut == "En attente")
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement planning: {ex.Message}", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadValidationsAsync()
        {
            try
            {
                var data = await _apiClient.GetAsync<List<ReservationDto>>("api/reservations/en-attente");
                _gridValidations.DataSource = data;
                
                _btnValidate.Enabled = data != null && data.Count > 0;
                _btnCancel.Enabled = data != null && data.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement validations: {ex.Message}", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ValidateSelectedAsync()
        {
            if (_gridValidations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show("Sélectionnez une réservation à valider.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Valider la réservation de {reservation.ClientNom} {reservation.ClientPrenom} ?\n\n" +
                $"Terrain: {reservation.TerrainNom}\n" +
                $"Date: {reservation.DateCreneau:dd/MM/yyyy}\n" +
                $"Horaire: {reservation.HeureDebut:hh\\:mm} - {reservation.HeureFin:hh\\:mm}\n" +
                $"Montant: {reservation.MontantTotal} DH", 
                "Confirmation de validation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                _btnValidate.Enabled = false;
                _btnValidate.Text = "Validation...";
                
                try
                {
                    var ok = await _apiClient.PutAsync($"api/reservations/{reservation.Id}/validate");
                    if (ok)
                    {
                        MessageBox.Show($"Réservation N°{reservation.Id} validée avec succès.", "Succès", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadValidationsAsync();
                        await LoadDashboardAsync();
                    }
                    else
                    {
                        MessageBox.Show("Validation impossible.", "Erreur", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    _btnValidate.Enabled = true;
                    _btnValidate.Text = "Valider";
                }
            }
        }

        private async Task CancelSelectedAsync()
        {
            if (_gridValidations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show("Sélectionnez une réservation à annuler.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Annuler la réservation de {reservation.ClientNom} {reservation.ClientPrenom} ?\n\n" +
                $"Terrain: {reservation.TerrainNom}\n" +
                $"Date: {reservation.DateCreneau:dd/MM/yyyy}\n" +
                $"Horaire: {reservation.HeureDebut:hh\\:mm} - {reservation.HeureFin:hh\\:mm}\n" +
                $"Montant: {reservation.MontantTotal} DH", 
                "Confirmation d'annulation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                _btnCancel.Enabled = false;
                _btnCancel.Text = "Annulation...";
                
                try
                {
                    var ok = await _apiClient.PutAsync($"api/reservations/{reservation.Id}/cancel");
                    if (ok)
                    {
                        MessageBox.Show($"Réservation N°{reservation.Id} annulée avec succès.", "Succès", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadValidationsAsync();
                        await LoadDashboardAsync();
                    }
                    else
                    {
                        MessageBox.Show("Annulation impossible.", "Erreur", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    _btnCancel.Enabled = true;
                    _btnCancel.Text = "Annuler";
                }
            }
        }
    }
}