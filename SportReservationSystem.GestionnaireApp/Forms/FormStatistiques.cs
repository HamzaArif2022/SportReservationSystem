using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.GestionnaireApp.Forms
{
    public partial class FormStatistiques : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient;

        public FormStatistiques(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();

            cmbPeriode.SelectedIndex = 1; // "Mois" par défaut

            Load += async (_, _) => await LoadDashboardAsync();
            cmbPeriode.SelectedIndexChanged += async (_, _) => await LoadChiffreAffairesAsync();
            btnExporter.Click += async (_, _) => await ExporterRapportAsync();
        }

        // ════════════════════════════════════════════════════════════════
        // DASHBOARD
        // ════════════════════════════════════════════════════════════════
        private async Task LoadDashboardAsync()
        {
            try
            {
                var dashboard = await _apiClient.GetAsync<DashboardStatsDto>(
                    "api/statistiques/dashboard");

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
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur chargement dashboard :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ════════════════════════════════════════════════════════════════
        // CHIFFRE D'AFFAIRES
        // ════════════════════════════════════════════════════════════════
        private async Task LoadChiffreAffairesAsync()
        {
            try
            {
                var periode = cmbPeriode.SelectedItem?.ToString() ?? "Mois";

                var (debut, fin) = periode switch
                {
                    "Semaine" => (DateTime.Today.AddDays(-7), DateTime.Today),
                    "Année" => (new DateTime(DateTime.Today.Year, 1, 1), DateTime.Today),
                    _ => (new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), DateTime.Today),
                };

                var ca = await _apiClient.GetAsync<ChiffreAffairesDto>(
                    $"api/statistiques/chiffre-affaires?debut={debut:yyyy-MM-dd}&fin={fin:yyyy-MM-dd}");

                if (ca != null)
                {
                    lblCATotal.Text = $"{ca.Total} DH";
                    lblCAEvolution.Text = $"{ca.Evolution}%";

                    dataGridViewCAMois.DataSource = ca.ParMois
                        .Select(kv => new { Mois = kv.Key, Montant = $"{kv.Value} DH" })
                        .ToList();

                    dataGridViewCATerrain.DataSource = ca.ParTerrain
                        .Select(kv => new { Terrain = kv.Key, Montant = $"{kv.Value} DH" })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur chargement CA :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ════════════════════════════════════════════════════════════════
        // EXPORT
        // ════════════════════════════════════════════════════════════════
        private async Task ExporterRapportAsync()
        {
            try
            {
                var debut = DateTime.Today.AddMonths(-1);
                var fin = DateTime.Today;
                var rapport = await _apiClient.GetFileAsync(
                    $"api/statistiques/export?debut={debut:yyyy-MM-dd}&fin={fin:yyyy-MM-dd}");

                if (rapport == null)
                {
                    MessageBox.Show(this,
                        "Aucune donnée à exporter.",
                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var dlg = new SaveFileDialog
                {
                    Filter = "Fichier texte (*.txt)|*.txt",
                    FileName = $"rapport_{debut:yyyyMMdd}_{fin:yyyyMMdd}.txt",
                };

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    await File.WriteAllBytesAsync(dlg.FileName, rapport);
                    MessageBox.Show(this,
                        "Rapport exporté avec succès.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur export :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}