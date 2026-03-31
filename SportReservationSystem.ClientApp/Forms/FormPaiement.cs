using System.Windows.Forms;
using MetroFramework.Forms;
using SportReservationSystem.ClientApp;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormPaiement : MetroForm
    {
        private readonly ApiClient _apiClient;
        private readonly int _clientId;
        private readonly int _reservationId;
        private readonly decimal _montant;

        public FormPaiement(ApiClient apiClient, int clientId, int reservationId, decimal montant)
        {
            _apiClient = apiClient;
            _clientId = clientId;
            _reservationId = reservationId;
            _montant = montant;
            InitializeComponent();
            
            txtReservationId.Text = _reservationId.ToString();
            txtMontant.Text = $"{_montant} DH";
            
            cmbModePaiement.SelectedIndex = 0;
            btnPayer.Click += async (_, _) => await PayerAsync();
        }

        private async Task PayerAsync()
        {
            var dto = new CreatePaiementDto
            {
                ReservationId = _reservationId,
                Montant = _montant,
                ModePaiement = cmbModePaiement.SelectedItem?.ToString() ?? "Carte"
            };
    
            // CORRECTION : Utiliser PostAsync<T> avec un seul type générique
            var paiement = await _apiClient.PostAsync<object>("api/paiements", dto);
    
            if (paiement != null)
            {
                MessageBox.Show("Paiement effectué avec succès! Un QR code vous a été envoyé par email.", 
                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Erreur lors du paiement.", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.lblReservation = new MetroFramework.Controls.MetroLabel();
                this.txtReservationId = new MetroFramework.Controls.MetroTextBox();
            this.lblMontant = new MetroFramework.Controls.MetroLabel();
            this.txtMontant = new MetroFramework.Controls.MetroTextBox();
            this.lblModePaiement = new MetroFramework.Controls.MetroLabel();
            this.cmbModePaiement = new MetroFramework.Controls.MetroComboBox();
            this.btnPayer = new MetroFramework.Controls.MetroButton();
            this.btnAnnuler = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // lblReservation
            // 
            this.lblReservation.AutoSize = true;
            this.lblReservation.Location = new System.Drawing.Point(30, 60);
            this.lblReservation.Name = "lblReservation";
            this.lblReservation.Size = new System.Drawing.Size(88, 20);
            this.lblReservation.TabIndex = 0;
            this.lblReservation.Text = "Réservation";
            // 
            // txtReservationId
            // 
            this.txtReservationId.Location = new System.Drawing.Point(150, 60);
            this.txtReservationId.Name = "txtReservationId";
            this.txtReservationId.ReadOnly = true;
            this.txtReservationId.Size = new System.Drawing.Size(150, 23);
            this.txtReservationId.TabIndex = 1;
            // 
            // lblMontant
            // 
            this.lblMontant.AutoSize = true;
            this.lblMontant.Location = new System.Drawing.Point(30, 100);
            this.lblMontant.Name = "lblMontant";
            this.lblMontant.Size = new System.Drawing.Size(61, 20);
            this.lblMontant.TabIndex = 2;
            this.lblMontant.Text = "Montant";
            // 
            // txtMontant
            // 
            this.txtMontant.Location = new System.Drawing.Point(150, 100);
            this.txtMontant.Name = "txtMontant";
            this.txtMontant.ReadOnly = true;
            this.txtMontant.Size = new System.Drawing.Size(150, 23);
            this.txtMontant.TabIndex = 3;
            // 
            // lblModePaiement
            // 
            this.lblModePaiement.AutoSize = true;
            this.lblModePaiement.Location = new System.Drawing.Point(30, 140);
            this.lblModePaiement.Name = "lblModePaiement";
            this.lblModePaiement.Size = new System.Drawing.Size(114, 20);
            this.lblModePaiement.TabIndex = 4;
            this.lblModePaiement.Text = "Mode Paiement";
            // 
            // cmbModePaiement
            // 
            this.cmbModePaiement.FormattingEnabled = true;
            this.cmbModePaiement.Items.AddRange(new object[] { "Carte", "Espèce", "Virement" });
            this.cmbModePaiement.Location = new System.Drawing.Point(150, 140);
            this.cmbModePaiement.Name = "cmbModePaiement";
            this.cmbModePaiement.Size = new System.Drawing.Size(150, 30);
            this.cmbModePaiement.TabIndex = 5;
            // 
            // btnPayer
            // 
            this.btnPayer.Location = new System.Drawing.Point(150, 200);
            this.btnPayer.Name = "btnPayer";
            this.btnPayer.Size = new System.Drawing.Size(100, 35);
            this.btnPayer.TabIndex = 6;
            this.btnPayer.Text = "Payer";
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Location = new System.Drawing.Point(260, 200);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(100, 35);
            this.btnAnnuler.TabIndex = 7;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.Click += (_, _) => Close();
            // 
            // FormPaiement
            // 
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnPayer);
            this.Controls.Add(this.cmbModePaiement);
            this.Controls.Add(this.lblModePaiement);
            this.Controls.Add(this.txtMontant);
            this.Controls.Add(this.lblMontant);
            this.Controls.Add(this.txtReservationId);
            this.Controls.Add(this.lblReservation);
            this.Text = "Paiement";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private MetroFramework.Controls.MetroLabel lblReservation;
        private MetroFramework.Controls.MetroTextBox txtReservationId;
        private MetroFramework.Controls.MetroLabel lblMontant;
        private MetroFramework.Controls.MetroTextBox txtMontant;
        private MetroFramework.Controls.MetroLabel lblModePaiement;
        private MetroFramework.Controls.MetroComboBox cmbModePaiement;
        private MetroFramework.Controls.MetroButton btnPayer;
        private MetroFramework.Controls.MetroButton btnAnnuler;
    }
}