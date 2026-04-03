using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormRechercheCreneaux : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient;
        private List<Terrain> _terrains = new();
        private List<Creneau> _creneaux = new();
        public Creneau? SelectedCreneau { get; private set; }

        public FormRechercheCreneaux(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();

            Load += async (_, _) => await LoadTerrainsAsync();
            btnRechercher.Click += async (_, _) => await RechercherAsync();
        }

        private async Task LoadTerrainsAsync()
        {
            try
            {
                _terrains = await _apiClient.GetAsync<List<Terrain>>("api/terrains") ?? new();
                cmbTerrain.DataSource = _terrains;
                cmbTerrain.DisplayMember = "Nom";
                cmbTerrain.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur chargement terrains :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RechercherAsync()
        {
            try
            {
                if (cmbTerrain.SelectedValue is not int terrainId) return;
                var date = dtpDate.Value.Date;

                _creneaux = await _apiClient.GetAsync<List<Creneau>>(
                    $"api/creneaux/disponibles?terrainId={terrainId}&date={date:yyyy-MM-dd}") ?? new();

                dataGridViewCreneaux.DataSource = _creneaux;
                lblStatus.Text = _creneaux.Count == 0
                    ? "Aucun créneau disponible pour cette date"
                    : $"{_creneaux.Count} créneau(x) disponible(s)";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur recherche :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}