using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;
using SportReservationSystem.ClientApp.Forms;
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
        
        // ============================================
        // BOUTONS DES FORMULAIRES - COMMENTÉS TEMPORAIREMENT
        // Décommentez après avoir ajouté les boutons dans le Designer
        // ============================================
        
        // _btnTerrains.Click += (_, _) => new FormTerrains(_apiClient).ShowDialog();
        // _btnRecherche.Click += async (_, _) =>
        // {
        //     var form = new FormRechercheCreneaux(_apiClient);
        //     if (form.ShowDialog() == DialogResult.OK && form.SelectedCreneau != null)
        //     {
        //         var reservationForm = new FormReservation(_apiClient, _clientId, form.SelectedCreneau);
        //         reservationForm.ShowDialog();
        //     }
        // };
        // _btnHistorique.Click += (_, _) => new FormHistorique(_apiClient, _clientId).ShowDialog();
        // _btnProfil.Click += (_, _) => new FormProfil(_apiClient, _clientId).ShowDialog();
    }

    private async Task LoadTerrainsAsync()
    {
        var terrains = await _apiClient.GetAsync<List<Terrain>>("api/terrains");
        _gridTerrains.DataSource = terrains;
    }

    private async Task LoadCreneauxAsync()
    {
        var date = _dateFilter.Value.ToString("yyyy-MM-dd");
        var creneaux = await _apiClient.GetAsync<List<Creneau>>($"api/creneaux/available?date={date}");
        _gridCreneaux.DataSource = creneaux;
    }

    private async Task LoadReservationsAsync()
    {
        var reservations = await _apiClient.GetAsync<List<ReservationDto>>($"api/reservations/client/{_clientId}");
        _gridReservations.DataSource = reservations;
    }

    private async Task BookSelectedAsync()
    {
        if (_gridCreneaux.CurrentRow?.DataBoundItem is not Creneau creneau)
        {
            MessageBox.Show("Selectionnez un creneau.");
            return;
        }

        await _apiClient.PostAsync<ReservationDto>("api/reservations", new
        {
            ClientId = _clientId,
            CreneauId = creneau.Id,
            Statut = "Confirmée"
        });
        await LoadCreneauxAsync();
        await LoadReservationsAsync();
    }

    private async Task CancelSelectedAsync()
    {
        if (_gridReservations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
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