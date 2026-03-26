using MetroFramework.Controls;
using MetroFramework.Forms;
using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.ClientApp;

public partial class FormMainClient : MetroForm
{
    private readonly ApiClient _apiClient;
    private readonly int _clientId;

    public FormMainClient(ApiClient apiClient, int clientId)
    {
        _apiClient = apiClient;
        _clientId = clientId;
        InitializeComponent();
        _btnSearch.Click += async (_, _) => await LoadCreneauxAsync();
        _btnBook.Click += async (_, _) => await BookSelectedAsync();
        _btnRefreshReservations.Click += async (_, _) => await LoadReservationsAsync();
        _btnCancelReservation.Click += async (_, _) => await CancelSelectedAsync();

        Load += async (_, _) =>
        {
            await LoadTerrainsAsync();
            await LoadCreneauxAsync();
            await LoadReservationsAsync();
        };
    }

    private async Task LoadTerrainsAsync()
    {
        _gridTerrains.DataSource = await _apiClient.GetAsync<List<Terrain>>("api/terrains");
    }

    private async Task LoadCreneauxAsync()
    {
        var date = _dateFilter.Value.ToString("yyyy-MM-dd");
        _gridCreneaux.DataSource = await _apiClient.GetAsync<List<Creneau>>($"api/creneaux/available?date={date}");
    }

    private async Task LoadReservationsAsync()
    {
        _gridReservations.DataSource = await _apiClient.GetAsync<List<Reservation>>($"api/reservations/client/{_clientId}");
    }

    private async Task BookSelectedAsync()
    {
        if (_gridCreneaux.CurrentRow?.DataBoundItem is not Creneau creneau)
        {
            MessageBox.Show("Selectionnez un creneau.");
            return;
        }

        await _apiClient.PostAsync<Reservation>("api/reservations", new ReservationDto
        {
            ClientId = _clientId,
            CreneauId = creneau.Id,
            Status = "Confirmée"
        });
        await LoadCreneauxAsync();
        await LoadReservationsAsync();
    }

    private async Task CancelSelectedAsync()
    {
        if (_gridReservations.CurrentRow?.DataBoundItem is not Reservation reservation)
        {
            MessageBox.Show("Selectionnez une reservation.");
            return;
        }

        var ok = await _apiClient.PutAsync($"api/reservations/{reservation.Id}/cancel");
        if (!ok)
        {
            MessageBox.Show("Impossible d'annuler.");
            return;
        }

        await LoadCreneauxAsync();
        await LoadReservationsAsync();
    }

    private void _tabs_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
