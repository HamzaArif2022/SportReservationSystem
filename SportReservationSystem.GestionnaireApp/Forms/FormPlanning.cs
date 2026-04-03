using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.GestionnaireApp.Forms
{
    public partial class FormPlanning : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient;

        public FormPlanning(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();

            dtpDate.Value = DateTime.Today;
            dtpDate.ValueChanged += async (_, _) => await LoadPlanningAsync();
            Load += async (_, _) => await LoadPlanningAsync();
        }

        // ════════════════════════════════════════════════════════════════
        // CHARGEMENT
        // ════════════════════════════════════════════════════════════════
        private async Task LoadPlanningAsync()
        {
            try
            {
                var date = dtpDate.Value.Date;
                var reservations = await _apiClient.GetAsync<List<ReservationDto>>(
                    $"api/reservations/planning?date={date:yyyy-MM-dd}");

                dataGridViewPlanning.DataSource = reservations;

                lblDate.Text = date.ToString("dddd dd MMMM yyyy",
                    System.Globalization.CultureInfo.GetCultureInfo("fr-FR"));
                lblCount.Text = $"{reservations?.Count ?? 0} réservation(s)";

                // Colorier par statut
                foreach (DataGridViewRow row in dataGridViewPlanning.Rows)
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
    }
}