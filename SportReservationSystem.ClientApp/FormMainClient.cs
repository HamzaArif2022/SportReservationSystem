using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SportReservationSystem.ClientApp.Forms;
using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.ClientApp
{
    public partial class FormMainClient : Form
    {
        private readonly ApiClient _apiClient;
        private readonly int _clientId;
        

        public FormMainClient(ApiClient apiClient, int clientId)
        {
            _apiClient = apiClient;
            _clientId = clientId;
            InitializeComponent();
            
            // Ajouter les boutons de navigation dans le panel
            AddNavigationButtons();
            
            // Événements
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

        private void AddNavigationButtons()
        {
            // Bouton Terrains
            var btnTerrains = new Button
            {
                Location = new Point(20, 10),
                Size = new Size(100, 32),
                Text = "Terrains",
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnTerrains.Click += (_, _) => new FormTerrains(_apiClient).ShowDialog();
            
            // Bouton Rechercher
            var btnRecherche = new Button
            {
                Location = new Point(130, 10),
                Size = new Size(100, 32),
                Text = "Rechercher",
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnRecherche.Click += async (_, _) =>
            {
                var form = new FormRechercheCreneaux(_apiClient);
                if (form.ShowDialog() == DialogResult.OK && form.SelectedCreneau != null)
                {
                    var reservationForm = new FormReservation(_apiClient, _clientId, form.SelectedCreneau);
                    reservationForm.ShowDialog();
                }
            };
            
            // Bouton Historique
            var btnHistorique = new Button
            {
                Location = new Point(240, 10),
                Size = new Size(100, 32),
                Text = "Historique",
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnHistorique.Click += (_, _) => new FormHistorique(_apiClient, _clientId).ShowDialog();
            
            // Bouton Mon Profil
            var btnProfil = new Button
            {
                Location = new Point(350, 10),
                Size = new Size(100, 32),
                Text = "Mon Profil",
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnProfil.Click += (_, _) => new FormProfil(_apiClient, _clientId).ShowDialog();
            
            _pnlNavigation.Controls.Add(btnTerrains);
            _pnlNavigation.Controls.Add(btnRecherche);
            _pnlNavigation.Controls.Add(btnHistorique);
            _pnlNavigation.Controls.Add(btnProfil);
        }

        private async Task LoadTerrainsAsync()
        {
            var terrains = await _apiClient.GetAsync<List<Terrain>>("api/terrains");
            _gridTerrains.DataSource = terrains;
        }

        private async Task LoadCreneauxAsync()
        {
            int terrainId = 1; // Par défaut, Stade Central
            var date = _dateFilter.Value.ToString("yyyy-MM-dd");
            
            var creneaux = await _apiClient.GetAsync<List<Creneau>>($"api/creneaux/disponibles?terrainId={terrainId}&date={date}");
            _gridCreneaux.DataSource = creneaux;
            
            _btnBook.Enabled = creneaux != null && creneaux.Count > 0;
        }

        private async Task LoadReservationsAsync()
        {
            var reservations = await _apiClient.GetAsync<List<ReservationDto>>("api/reservations/mes-reservations");
            _gridReservations.DataSource = reservations;
            
            foreach (DataGridViewRow row in _gridReservations.Rows)
            {
                if (row.DataBoundItem is ReservationDto reservation)
                {
                    if (reservation.Statut == "Confirmée")
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    else if (reservation.Statut == "En attente")
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    else if (reservation.Statut == "Annulée" || reservation.Statut == "Refusée")
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                }
            }
            
            _btnCancelReservation.Enabled = _gridReservations.Rows.Count > 0;
        }

       private async Task BookSelectedAsync()
{
    if (_gridCreneaux.CurrentRow?.DataBoundItem is not Creneau creneau)
    {
        MessageBox.Show("Sélectionnez un créneau.", "Information", 
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
    }

    decimal montant = creneau.Terrain?.TarifHoraire ?? 0;

    var result = MessageBox.Show(
        $"Réserver le créneau ?\n\n" +
        $"Terrain: {creneau.Terrain?.Nom}\n" +
        $"Date: {creneau.Date:dd/MM/yyyy}\n" +
        $"Horaire: {creneau.HeureDebut:hh\\:mm} - {creneau.HeureFin:hh\\:mm}\n" +
        $"Montant: {montant} DH", 
        "Confirmation de réservation", 
        MessageBoxButtons.YesNo, 
        MessageBoxIcon.Question);
    
    if (result == DialogResult.Yes)
    {
        _btnBook.Enabled = false;
        _btnBook.Text = "Réservation...";
        
        try
        {
            var dto = new CreateReservationDto
            {
                CreneauId = creneau.Id,
                MontantTotal = montant
            };
            
            var reservation = await _apiClient.PostAsync<ReservationDto>("api/reservations", dto);
            
            if (reservation != null)
            {
                MessageBox.Show($"Réservation créée avec succès!\n\nMontant: {reservation.MontantTotal} DH\nVeuillez procéder au paiement.", 
                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                var paiementForm = new FormPaiement(_apiClient, _clientId, reservation.Id, reservation.MontantTotal);
                paiementForm.ShowDialog();
                
                await LoadCreneauxAsync();
                await LoadReservationsAsync();
            }
            else
            {
                MessageBox.Show("Erreur lors de la création de la réservation.", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur: {ex.Message}", "Erreur", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _btnBook.Enabled = true;
            _btnBook.Text = "Réserver";
        }
    }
}

        private async Task CancelSelectedAsync()
        {
            if (_gridReservations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show("Sélectionnez une réservation.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            if (reservation.Statut != "Confirmée" && reservation.Statut != "En attente")
            {
                MessageBox.Show("Seules les réservations confirmées ou en attente peuvent être annulées.", 
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var result = MessageBox.Show($"Annuler la réservation du {reservation.DateCreneau:dd/MM/yyyy} ?\nMontant: {reservation.MontantTotal} DH", 
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                _btnCancelReservation.Enabled = false;
                _btnCancelReservation.Text = "Annulation...";
                
                try
                {
                    var success = await _apiClient.DeleteAsync($"api/reservations/{reservation.Id}");
                    
                    if (success)
                    {
                        MessageBox.Show("Réservation annulée. Remboursement effectué.", "Succès", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadCreneauxAsync();
                        await LoadReservationsAsync();
                    }
                    else
                    {
                        MessageBox.Show("Impossible d'annuler (délai dépassé).", "Erreur", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                finally
                {
                    _btnCancelReservation.Enabled = true;
                    _btnCancelReservation.Text = "Annuler";
                }
            }
        }

        private void _tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_tabs.SelectedTab == _tabTerrains)
                LoadTerrainsAsync();
            else if (_tabs.SelectedTab == _tabCreneaux)
                LoadCreneauxAsync();
            else if (_tabs.SelectedTab == _tabReservations)
                LoadReservationsAsync();
        }
    }
}