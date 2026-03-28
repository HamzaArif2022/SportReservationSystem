using System.Collections.Generic;
using MetroFramework.Forms;
using SportReservationSystem.ClientApp;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormTerrains : MetroForm
    {
        private readonly ApiClient _apiClient;

        public FormTerrains(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            Load += async (_, _) => await LoadTerrainsAsync();
        }

        private async Task LoadTerrainsAsync()
        {
            var terrains = await _apiClient.GetAsync<List<Terrain>>("api/terrains");
            dataGridViewTerrains.DataSource = terrains;
        }

        private void InitializeComponent()
        {
            this.dataGridViewTerrains = new System.Windows.Forms.DataGridView();
            this.lblTitle = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTerrains)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTerrains
            // 
            this.dataGridViewTerrains.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTerrains.Location = new System.Drawing.Point(23, 80);
            this.dataGridViewTerrains.Name = "dataGridViewTerrains";
            this.dataGridViewTerrains.RowHeadersWidth = 51;
            this.dataGridViewTerrains.RowTemplate.Height = 24;
            this.dataGridViewTerrains.Size = new System.Drawing.Size(754, 350);
            this.dataGridViewTerrains.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblTitle.Location = new System.Drawing.Point(23, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Liste des Terrains";
            // 
            // FormTerrains
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dataGridViewTerrains);
            this.Text = "Terrains";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTerrains)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dataGridViewTerrains;
        private MetroFramework.Controls.MetroLabel lblTitle;
    }
}