using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Controls;
using SportReservationSystem.ClientApp.Forms;
using SportReservationSystem.Shared.DTOs;
using SportReservationSystem.Shared.Models;

namespace SportReservationSystem.ClientApp
{
    // ⚠️ Hérite de MetroForm pour activer le thème Dark/Blue
    public partial class FormMainClient : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient;
        private readonly int _clientId;

        public FormMainClient(ApiClient apiClient, int clientId)
        {
            _apiClient = apiClient;
            _clientId = clientId;

            InitializeComponent();
            AddNavigationButtons();

            // Événements boutons
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

        // ════════════════════════════════════════════════════════════════
        // NAVIGATION — boutons créés dynamiquement avec style Metro
        // ════════════════════════════════════════════════════════════════
        private void AddNavigationButtons()
        {
            var btnTerrains = CreateNavButton("Terrains", 10);
            btnTerrains.Click += (_, _) => new FormTerrains(_apiClient).ShowDialog();

            var btnRecherche = CreateNavButton("Rechercher", 120);
            btnRecherche.Click += async (_, _) =>
            {
                var form = new FormRechercheCreneaux(_apiClient);
                if (form.ShowDialog() == DialogResult.OK && form.SelectedCreneau != null)
                    new FormReservation(_apiClient, _clientId, form.SelectedCreneau).ShowDialog();
            };

            var btnHistorique = CreateNavButton("Historique", 230);
            btnHistorique.Click += (_, _) => new FormHistorique(_apiClient, _clientId).ShowDialog();

            var btnProfil = CreateNavButton("Mon Profil", 340);
            btnProfil.Click += (_, _) => new FormProfil(_apiClient, _clientId).ShowDialog();

            _pnlNavigation.Controls.Add(btnTerrains);
            _pnlNavigation.Controls.Add(btnRecherche);
            _pnlNavigation.Controls.Add(btnHistorique);
            _pnlNavigation.Controls.Add(btnProfil);
        }

        /// <summary>Crée un MetroButton stylé pour la barre de navigation.</summary>
        private MetroButton CreateNavButton(string text, int x)
        {
            return new MetroButton
            {
                Text = text,
                Location = new Point(x, 9),
                Size = new Size(102, 34),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };
        }

        // ════════════════════════════════════════════════════════════════
        // CHARGEMENT DES DONNÉES
        // ════════════════════════════════════════════════════════════════
        private async Task LoadTerrainsAsync()
        {
            var terrains = await _apiClient.GetAsync<List<Terrain>>("api/terrains");
            _gridTerrains.DataSource = terrains;
        }

        private async Task LoadCreneauxAsync()
        {
            try
            {
                // On désactive le bouton en attendant le chargement
                _btnBook.Enabled = false;

                int terrainId = 0; // À adapter si vous avez un filtre par terrain plus tard
                var date = _dateFilter.Value.ToString("yyyy-MM-dd");

                // Utilisation de CreneauDto !
                var creneaux = await _apiClient.GetAsync<List<CreneauDto>>(
                    $"api/creneaux/disponibles?terrainId={terrainId}&date={date}");

                if (creneaux != null && creneaux.Count > 0)
                {
                    _gridCreneaux.DataSource = creneaux;
                    FormatCreneauxColumns(); 
                    _btnBook.Enabled = true; 
                }
                else
                {
                    _gridCreneaux.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Erreur lors du chargement des créneaux : " + ex.Message,
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Méthode dédiée pour garder votre code propre
        private void FormatCreneauxColumns()
        {
            if (_gridCreneaux.Columns.Count == 0) return;

            // 1. Cacher les colonnes techniques et redondantes
            _gridCreneaux.Columns["Id"].Visible = false;
            _gridCreneaux.Columns["TerrainId"].Visible = false;
            _gridCreneaux.Columns["Date"].Visible = false;
            _gridCreneaux.Columns["HeureDebut"].Visible = false;
            _gridCreneaux.Columns["HeureFin"].Visible = false;
            _gridCreneaux.Columns["EstDisponible"].Visible = false;

            // 2. Renommer les colonnes visibles pour l'utilisateur
            _gridCreneaux.Columns["TerrainNom"].HeaderText = "Terrain";
            _gridCreneaux.Columns["TypeSport"].HeaderText = "Sport";
            _gridCreneaux.Columns["Capacite"].HeaderText = "Capacité Max";

            // Formatage pour afficher la devise (ex: "25.00 €")
            _gridCreneaux.Columns["TarifHoraire"].HeaderText = "Tarif/Heure";
            _gridCreneaux.Columns["TarifHoraire"].DefaultCellStyle.Format = "C2";

            _gridCreneaux.Columns["DateHeure"].HeaderText = "Date et Heure";

            // 3. Réorganiser l'ordre d'affichage (Optionnel)
            _gridCreneaux.Columns["TerrainNom"].DisplayIndex = 0;
            _gridCreneaux.Columns["TypeSport"].DisplayIndex = 1;
            _gridCreneaux.Columns["DateHeure"].DisplayIndex = 2;
            _gridCreneaux.Columns["Capacite"].DisplayIndex = 3;
            _gridCreneaux.Columns["TarifHoraire"].DisplayIndex = 4;
        }

        private async Task LoadReservationsAsync()
        {
            var reservations = await _apiClient.GetAsync<List<ReservationDto>>(
                "api/reservations/mes-reservations");

            _gridReservations.DataSource = reservations;
            ApplyReservationRowColors();

            _btnCancelReservation.Enabled = _gridReservations.Rows.Count > 0;
        }

        /// <summary>Colore les lignes selon le statut de la réservation.</summary>
        private void ApplyReservationRowColors()
        {
            foreach (DataGridViewRow row in _gridReservations.Rows)
            {
                if (row.DataBoundItem is not ReservationDto r) continue;

                row.DefaultCellStyle.BackColor = r.Statut switch
                {
                    "Confirmée" => Color.FromArgb(30, 80, 30),   // vert sombre
                    "En attente" => Color.FromArgb(70, 60, 10),   // jaune sombre
                    "Annulée" or "Refusée" => Color.FromArgb(80, 20, 20),   // rouge sombre
                    _ => Color.FromArgb(20, 20, 20),
                };
                row.DefaultCellStyle.ForeColor = Color.White;
            }
        }

        // ════════════════════════════════════════════════════════════════
        // ACTION — RÉSERVER
        // ════════════════════════════════════════════════════════════════
        private async Task BookSelectedAsync()
        {
            if (_gridCreneaux.CurrentRow?.DataBoundItem is not CreneauDto selectedCreneau)
            {
                MessageBox.Show(
                    "Veuillez sélectionner un créneau dans la liste.",
                    "Sélection requise",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal montant = selectedCreneau.TarifHoraire;

            var confirm = MessageBox.Show(
                $"Terrain : {selectedCreneau.TerrainNom}\n" +
                $"Date    : {selectedCreneau.Date:dd/MM/yyyy}\n" +
                $"Horaire : {selectedCreneau.HeureDebut:hh\\:mm} – {selectedCreneau.HeureFin:hh\\:mm}\n" +
                $"Montant : {montant} DH\n\nConfirmer la réservation ?",
                "Confirmation de réservation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            _btnBook.Enabled = false;
            _btnBook.Text = "En cours...";

            try
            {
                var dto = new CreateReservationDto
                {
                    CreneauId = selectedCreneau.Id,
                    MontantTotal = montant,
                };

                var reservation = await _apiClient.PostAsync<ReservationDto>("api/reservations", dto);

                if (reservation != null)
                {
                    MessageBox.Show(
                        $"Réservation créée avec succès !\nMontant : {reservation.MontantTotal} DH\n\nVeuillez procéder au paiement.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    new FormPaiement(_apiClient, _clientId, reservation.Id, reservation.MontantTotal)
                        .ShowDialog();

                    await LoadCreneauxAsync();
                    await LoadReservationsAsync();

                }
                else
                {
                    MessageBox.Show(
                        "Erreur lors de la création de la réservation.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Erreur inattendue :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _btnBook.Enabled = true;
                _btnBook.Text = "Réserver";
            }
        }

        // ════════════════════════════════════════════════════════════════
        // ACTION — ANNULER RÉSERVATION
        // ════════════════════════════════════════════════════════════════
        private async Task CancelSelectedAsync()
        {
            if (_gridReservations.CurrentRow?.DataBoundItem is not ReservationDto reservation)
            {
                MessageBox.Show(this,
                    "Veuillez sélectionner une réservation.",
                    "Sélection requise", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (reservation.Statut != "Confirmée" && reservation.Statut != "En attente")
            {
                MessageBox.Show(this,
                    "Seules les réservations confirmées ou en attente peuvent être annulées.",
                    "Action impossible", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(this,
                $"Annuler la réservation du {reservation.DateCreneau:dd/MM/yyyy} ?\nMontant : {reservation.MontantTotal} DH",
                "Confirmation d'annulation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            _btnCancelReservation.Enabled = false;
            _btnCancelReservation.Text = "Annulation...";

            try
            {
                var success = await _apiClient.DeleteAsync($"api/reservations/{reservation.Id}");

                if (success)
                {
                    MessageBox.Show(this,
                        "Réservation annulée. Remboursement effectué.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await LoadCreneauxAsync();
                    await LoadReservationsAsync();
                }
                else
                {
                    MessageBox.Show(this,
                        "Impossible d'annuler cette réservation (délai dépassé).",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                _btnCancelReservation.Enabled = true;
                _btnCancelReservation.Text = "Annuler";
            }
        }

        // ════════════════════════════════════════════════════════════════
        // ÉVÉNEMENT — CHANGEMENT D'ONGLET
        // ════════════════════════════════════════════════════════════════
        private void _tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_tabs.SelectedTab == _tabTerrains) _ = LoadTerrainsAsync();
            else if (_tabs.SelectedTab == _tabCreneaux) _ = LoadCreneauxAsync();
            else if (_tabs.SelectedTab == _tabReservations) _ = LoadReservationsAsync();
        }
    }
}