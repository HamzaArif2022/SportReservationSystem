using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormTerrains : MetroFramework.Forms.MetroForm
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
            try
            {
                var terrains = await _apiClient.GetAsync<List<Terrain>>("api/terrains");
                dataGridViewTerrains.DataSource = terrains;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur chargement terrains :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}