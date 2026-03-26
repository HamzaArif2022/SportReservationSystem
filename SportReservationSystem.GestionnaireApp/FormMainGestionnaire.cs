using MetroFramework.Controls;
using MetroFramework.Forms;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.GestionnaireApp;

public partial class FormMainGestionnaire : MetroForm
{
    private readonly ApiClient _apiClient;

    public FormMainGestionnaire(ApiClient apiClient)
    {
        _apiClient = apiClient;
        InitializeComponent();
        _btnRefreshDashboard.Click += async (_, _) => await LoadReservationsAsync();
        _btnRefresh.Click += async (_, _) => await LoadReservationsAsync();
        _btnValidate.Click += async (_, _) => await ValidateSelectedAsync();
        _btnCancel.Click += async (_, _) => await CancelSelectedAsync();

        Load += async (_, _) => await LoadReservationsAsync();
    }

    private async Task LoadReservationsAsync()
    {
        var data = await _apiClient.GetAsync<List<Reservation>>("api/reservations");
        _gridDashboard.DataSource = data;
        _gridValidations.DataSource = data?.ToList();
    }

    private async Task ValidateSelectedAsync()
    {
        if (_gridValidations.CurrentRow?.DataBoundItem is not Reservation reservation)
        {
            MessageBox.Show("Selectionnez une reservation.");
            return;
        }

        var ok = await _apiClient.PutAsync($"api/reservations/{reservation.Id}/validate");
        if (!ok)
        {
            MessageBox.Show("Validation impossible.");
        }

        await LoadReservationsAsync();
    }

    private async Task CancelSelectedAsync()
    {
        if (_gridValidations.CurrentRow?.DataBoundItem is not Reservation reservation)
        {
            MessageBox.Show("Selectionnez une reservation.");
            return;
        }

        var ok = await _apiClient.PutAsync($"api/reservations/{reservation.Id}/cancel");
        if (!ok)
        {
            MessageBox.Show("Annulation impossible.");
        }

        await LoadReservationsAsync();
    }
}
