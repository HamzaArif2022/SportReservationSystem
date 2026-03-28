using MetroFramework.Forms;
using SportReservationSystem.GestionnaireApp.Services;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.GestionnaireApp.Forms
{
    public partial class FormPlanning : MetroForm
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

        private async Task LoadPlanningAsync()
        {
            var date = dtpDate.Value.Date;
            var reservations = await _apiClient.GetAsync<List<ReservationDto>>($"api/reservations/planning?date={date:yyyy-MM-dd}");
            
            dataGridViewPlanning.DataSource = reservations;
            lblDate.Text = date.ToString("dddd dd MMMM yyyy");
            lblCount.Text = $"{reservations.Count} réservation(s)";
        }

        private void InitializeComponent()
        {
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewPlanning = new System.Windows.Forms.DataGridView();
            this.lblDate = new MetroFramework.Controls.MetroLabel();
            this.lblCount = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanning)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(23, 40);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(250, 22);
            this.dtpDate.TabIndex = 0;
            // 
            // dataGridViewPlanning
            // 
            this.dataGridViewPlanning.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlanning.Location = new System.Drawing.Point(23, 80);
            this.dataGridViewPlanning.Name = "dataGridViewPlanning";
            this.dataGridViewPlanning.RowHeadersWidth = 51;
            this.dataGridViewPlanning.RowTemplate.Height = 24;
            this.dataGridViewPlanning.Size = new System.Drawing.Size(954, 350);
            this.dataGridViewPlanning.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblDate.Location = new System.Drawing.Point(280, 40);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 25);
            this.lblDate.TabIndex = 2;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(23, 440);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 20);
            this.lblCount.TabIndex = 3;
            // 
            // FormPlanning
            // 
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dataGridViewPlanning);
            this.Controls.Add(this.dtpDate);
            this.Text = "Planning des Réservations";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlanning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.DataGridView dataGridViewPlanning;
        private MetroFramework.Controls.MetroLabel lblDate;
        private MetroFramework.Controls.MetroLabel lblCount;
    }
}