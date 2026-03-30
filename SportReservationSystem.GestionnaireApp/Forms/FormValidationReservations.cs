using MetroFramework.Forms;
using SportReservationSystem.GestionnaireApp;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.GestionnaireApp.Forms
{
    public partial class FormValidationReservations : MetroForm
    {
        private readonly ApiClient _apiClient;
        private List<ReservationDto> _reservations;

        public FormValidationReservations(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            Load += async (_, _) => await LoadReservationsAsync();
            
            btnValider.Click += async (_, _) => await ValiderReservationAsync();
            btnRefuser.Click += async (_, _) => await RefuserReservationAsync();
            btnRafraichir.Click += async (_, _) => await LoadReservationsAsync();
        }

        private async Task LoadReservationsAsync()
        {
            _reservations = await _apiClient.GetAsync<List<ReservationDto>>("api/reservations/en-attente");
            dataGridViewReservations.DataSource = _reservations;
            lblCount.Text = $"{_reservations.Count} réservation(s) en attente";
            
            if (_reservations.Count == 0)
            {
                btnValider.Enabled = false;
                btnRefuser.Enabled = false;
            }
            else
            {
                btnValider.Enabled = true;
                btnRefuser.Enabled = true;
            }
        }

        private async Task ValiderReservationAsync()
        {
            if (dataGridViewReservations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show("Sélectionnez une réservation à valider.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var result = MessageBox.Show($"Valider la réservation de {reservation.ClientNom} {reservation.ClientPrenom} ?", 
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                var success = await _apiClient.PutAsync($"api/reservations/{reservation.Id}/validate", null);
                
                if (success)
                {
                    MessageBox.Show("Réservation validée avec succès.", "Succès", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadReservationsAsync();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la validation.", "Erreur", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task RefuserReservationAsync()
        {
            if (dataGridViewReservations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show("Sélectionnez une réservation à refuser.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var motif = Microsoft.VisualBasic.Interaction.InputBox("Motif du refus :", "Refus de réservation", "");
            
            if (string.IsNullOrWhiteSpace(motif))
            {
                MessageBox.Show("Un motif est requis pour refuser la réservation.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var success = await _apiClient.PutAsync($"api/reservations/{reservation.Id}/refuse", new { motif });
            
            if (success)
            {
                MessageBox.Show("Réservation refusée avec succès.", "Succès", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadReservationsAsync();
            }
            else
            {
                MessageBox.Show("Erreur lors du refus.", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.dataGridViewReservations = new System.Windows.Forms.DataGridView();
            this.btnValider = new MetroFramework.Controls.MetroButton();
            this.btnRefuser = new MetroFramework.Controls.MetroButton();
            this.btnRafraichir = new MetroFramework.Controls.MetroButton();
            this.lblCount = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReservations)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewReservations
            // 
            this.dataGridViewReservations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReservations.Location = new System.Drawing.Point(23, 80);
            this.dataGridViewReservations.Name = "dataGridViewReservations";
            this.dataGridViewReservations.RowHeadersWidth = 51;
            this.dataGridViewReservations.RowTemplate.Height = 24;
            this.dataGridViewReservations.Size = new System.Drawing.Size(954, 350);
            this.dataGridViewReservations.TabIndex = 0;
            // 
            // btnValider
            // 
            this.btnValider.Location = new System.Drawing.Point(23, 450);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(100, 35);
            this.btnValider.TabIndex = 1;
            this.btnValider.Text = "Valider";
            // 
            // btnRefuser
            // 
            this.btnRefuser.Location = new System.Drawing.Point(130, 450);
            this.btnRefuser.Name = "btnRefuser";
            this.btnRefuser.Size = new System.Drawing.Size(100, 35);
            this.btnRefuser.TabIndex = 2;
            this.btnRefuser.Text = "Refuser";
            // 
            // btnRafraichir
            // 
            this.btnRafraichir.Location = new System.Drawing.Point(877, 450);
            this.btnRafraichir.Name = "btnRafraichir";
            this.btnRafraichir.Size = new System.Drawing.Size(100, 35);
            this.btnRafraichir.TabIndex = 3;
            this.btnRafraichir.Text = "Rafraîchir";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblCount.Location = new System.Drawing.Point(23, 40);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 25);
            this.lblCount.TabIndex = 4;
            // 
            // FormValidationReservations
            // 
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnRafraichir);
            this.Controls.Add(this.btnRefuser);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.dataGridViewReservations);
            this.Text = "Validation des Réservations";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReservations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dataGridViewReservations;
        private MetroFramework.Controls.MetroButton btnValider;
        private MetroFramework.Controls.MetroButton btnRefuser;
        private MetroFramework.Controls.MetroButton btnRafraichir;
        private MetroFramework.Controls.MetroLabel lblCount;
    }
}