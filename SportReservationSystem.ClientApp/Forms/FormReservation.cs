using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormReservation : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient;
        private readonly int _clientId;
        private readonly Creneau _selectedCreneau;

        public FormReservation(ApiClient apiClient, int clientId, Creneau creneau)
        {
            _apiClient = apiClient;
            _clientId = clientId;
            _selectedCreneau = creneau;
            InitializeComponent();

            txtTerrain.Text = creneau.Terrain?.Nom ?? "Non spécifié";
            txtDate.Text = creneau.Date.ToString("dd/MM/yyyy");
            txtHeure.Text = $"{creneau.HeureDebut:hh\\:mm} – {creneau.HeureFin:hh\\:mm}";
            txtTarif.Text = $"{creneau.Terrain?.TarifHoraire} DH/h";
            txtTotal.Text = $"{creneau.Terrain?.TarifHoraire} DH";

            btnConfirmer.Click += async (_, _) => await ConfirmerAsync();
            btnAnnuler.Click += (_, _) => Close();
        }

        private async Task ConfirmerAsync()
        {
            btnConfirmer.Enabled = false;
            btnConfirmer.Text = "Création...";
            try
            {
                var reservation = await _apiClient.PostAsync<ReservationDto>("api/reservations",
                    new CreateReservationDto
                    {
                        CreneauId = _selectedCreneau.Id,
                        MontantTotal = _selectedCreneau.Terrain?.TarifHoraire ?? 0,
                    });

                if (reservation != null)
                {
                    MessageBox.Show(this,
                        "Réservation créée ! Procédez au paiement.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    new FormPaiement(_apiClient, _clientId, reservation.Id, reservation.MontantTotal)
                        .ShowDialog();
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(this,
                        "Erreur lors de la création de la réservation.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnConfirmer.Enabled = true;
                btnConfirmer.Text = "✔  Confirmer et payer";
            }
        }
    }
}