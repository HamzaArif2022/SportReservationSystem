using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormPaiement : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient;
        private readonly int _clientId;
        private readonly int _reservationId;
        private readonly decimal _montant;

        //private static readonly Color Gold = Color.FromArgb(251, 191, 36);
        //private static readonly Color GoldDim = Color.FromArgb(140, 105, 15);
        private static readonly Color TextDark = Color.FromArgb(12, 12, 12);

        public FormPaiement(ApiClient apiClient, int clientId, int reservationId, decimal montant)
        {
            _apiClient = apiClient;
            _clientId = clientId;
            _reservationId = reservationId;
            _montant = montant;
            InitializeComponent();

            txtReservationId.Text = $"#{_reservationId}";
            txtMontant.Text = $"{_montant} DH";
            lblMontantVal.Text = $"Total à régler : {_montant} DH";

            cmbModePaiement.SelectedIndex = 0;
            btnPayer.Click += async (_, _) => await PayerAsync();
            btnAnnuler.Click += (_, _) => Close();
        }

        private async Task PayerAsync()
        {
            SetPayState(loading: true);
            try
            {
                var dto = new CreatePaiementDto
                {
                    ReservationId = _reservationId,
                    Montant = _montant,
                    ModePaiement = cmbModePaiement.SelectedItem?.ToString() ?? "Carte",
                };

                var paiement = await _apiClient.PostAsync<object>("api/paiements", dto);

                if (paiement != null)
                {
                    
                    MessageBox.Show(this,
                        "Paiement effectué avec succès !\nUn QR code vous a été envoyé par email.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    
                    MessageBox.Show(this,
                        "Erreur lors du paiement.",
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
                SetPayState(loading: false);
            }
        }

        private void SetPayState(bool loading)
        {
            btnPayer.Enabled = !loading;
            btnPayer.Text = loading ? "Paiement en cours..." : "💳  Payer maintenant";
            btnPayer.BackColor = loading ? GoldDim : Gold;
            btnPayer.ForeColor = TextDark;
        }
    }
}