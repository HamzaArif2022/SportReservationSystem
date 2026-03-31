using System.Windows.Forms;

namespace SportReservationSystem.ClientApp
{
    partial class FormMainClient
    {
        private TabControl _tabs = null!;
        private TabPage _tabTerrains = null!;
        private TabPage _tabCreneaux = null!;
        private TabPage _tabReservations = null!;
        private DataGridView _gridTerrains = null!;
        private DataGridView _gridCreneaux = null!;
        private DataGridView _gridReservations = null!;
        private DateTimePicker _dateFilter = null!;
        private Button _btnSearch = null!;
        private Button _btnBook = null!;
        private Button _btnRefreshReservations = null!;
        private Button _btnCancelReservation = null!;
        private Panel _pnlSearch = null!;
        private Panel _pnlReservations = null!;
        private Panel _pnlNavigation = null!;  // Panel de navigation en haut

        private void InitializeComponent()
        {
            _tabs = new TabControl();
            _tabTerrains = new TabPage();
            _gridTerrains = new DataGridView();
            _tabCreneaux = new TabPage();
            _gridCreneaux = new DataGridView();
            _pnlSearch = new Panel();
            _dateFilter = new DateTimePicker();
            _btnSearch = new Button();
            _btnBook = new Button();
            _tabReservations = new TabPage();
            _gridReservations = new DataGridView();
            _pnlReservations = new Panel();
            _btnRefreshReservations = new Button();
            _btnCancelReservation = new Button();
            _pnlNavigation = new Panel();
            
            _tabs.SuspendLayout();
            _tabTerrains.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_gridTerrains).BeginInit();
            _tabCreneaux.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_gridCreneaux).BeginInit();
            _pnlSearch.SuspendLayout();
            _tabReservations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_gridReservations).BeginInit();
            _pnlReservations.SuspendLayout();
            _pnlNavigation.SuspendLayout();
            SuspendLayout();
            
            // ============================================
            // PANEL DE NAVIGATION (en haut)
            // ============================================
            _pnlNavigation.Dock = DockStyle.Top;
            _pnlNavigation.Height = 50;
            _pnlNavigation.BackColor = Color.FromArgb(0, 120, 215);
            
            // Boutons de navigation (ajoutés dans le code)
            // Les boutons seront créés dans FormMainClient.cs
            
            // ============================================
            // TAB CONTROL (en dessous du panel de navigation)
            // ============================================
            _tabs.Dock = DockStyle.Fill;
            _tabs.Controls.Add(_tabTerrains);
            _tabs.Controls.Add(_tabCreneaux);
            _tabs.Controls.Add(_tabReservations);
            _tabs.Location = new Point(0, 50);  // Décalé de 50px pour laisser la place au panel
            _tabs.Size = new Size(1000, 650);
            _tabs.SelectedIndexChanged += _tabs_SelectedIndexChanged;
            
            // ============================================
            // TabPage Terrains
            // ============================================
            _tabTerrains.Controls.Add(_gridTerrains);
            _tabTerrains.Location = new Point(4, 27);
            _tabTerrains.Size = new Size(992, 619);
            _tabTerrains.Text = "Terrains";
            
            _gridTerrains.Dock = DockStyle.Fill;
            _gridTerrains.AllowUserToResizeRows = false;
            _gridTerrains.BackgroundColor = Color.White;
            _gridTerrains.BorderStyle = BorderStyle.FixedSingle;
            _gridTerrains.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _gridTerrains.ReadOnly = true;
            _gridTerrains.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            // ============================================
            // TabPage Créneaux
            // ============================================
            _tabCreneaux.Controls.Add(_gridCreneaux);
            _tabCreneaux.Controls.Add(_pnlSearch);
            _tabCreneaux.Location = new Point(4, 27);
            _tabCreneaux.Size = new Size(992, 619);
            _tabCreneaux.Text = "Créneaux";
            
            // Panel de recherche (en haut de l'onglet)
            _pnlSearch.Dock = DockStyle.Top;
            _pnlSearch.Height = 60;
            _pnlSearch.Padding = new Padding(10);
            _pnlSearch.BackColor = Color.FromArgb(245, 245, 245);
            
            // DateTimePicker
            _dateFilter.Location = new Point(10, 15);
            _dateFilter.Size = new Size(200, 27);
            _dateFilter.Format = DateTimePickerFormat.Short;
            
            // Bouton Rechercher
            _btnSearch.Location = new Point(220, 13);
            _btnSearch.Size = new Size(120, 32);
            _btnSearch.Text = "Rechercher";
            _btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            _btnSearch.ForeColor = Color.White;
            _btnSearch.FlatStyle = FlatStyle.Flat;
            _btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            
            // Bouton Réserver
            _btnBook.Location = new Point(350, 13);
            _btnBook.Size = new Size(120, 32);
            _btnBook.Text = "Réserver";
            _btnBook.BackColor = Color.FromArgb(0, 120, 215);
            _btnBook.ForeColor = Color.White;
            _btnBook.FlatStyle = FlatStyle.Flat;
            _btnBook.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            
            _pnlSearch.Controls.Add(_dateFilter);
            _pnlSearch.Controls.Add(_btnSearch);
            _pnlSearch.Controls.Add(_btnBook);
            
            // DataGridView Créneaux
            _gridCreneaux.Dock = DockStyle.Fill;
            _gridCreneaux.AllowUserToResizeRows = false;
            _gridCreneaux.BackgroundColor = Color.White;
            _gridCreneaux.BorderStyle = BorderStyle.FixedSingle;
            _gridCreneaux.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _gridCreneaux.ReadOnly = true;
            _gridCreneaux.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            // ============================================
            // TabPage Réservations
            // ============================================
            _tabReservations.Controls.Add(_gridReservations);
            _tabReservations.Controls.Add(_pnlReservations);
            _tabReservations.Location = new Point(4, 27);
            _tabReservations.Size = new Size(992, 619);
            _tabReservations.Text = "Réservations";
            
            // Panel des actions
            _pnlReservations.Dock = DockStyle.Top;
            _pnlReservations.Height = 60;
            _pnlReservations.Padding = new Padding(10);
            _pnlReservations.BackColor = Color.FromArgb(245, 245, 245);
            
            // Bouton Actualiser
            _btnRefreshReservations.Location = new Point(10, 13);
            _btnRefreshReservations.Size = new Size(100, 32);
            _btnRefreshReservations.Text = "Actualiser";
            _btnRefreshReservations.BackColor = Color.FromArgb(102, 102, 102);
            _btnRefreshReservations.ForeColor = Color.White;
            _btnRefreshReservations.FlatStyle = FlatStyle.Flat;
            
            // Bouton Annuler
            _btnCancelReservation.Location = new Point(120, 13);
            _btnCancelReservation.Size = new Size(100, 32);
            _btnCancelReservation.Text = "Annuler";
            _btnCancelReservation.BackColor = Color.FromArgb(232, 17, 35);
            _btnCancelReservation.ForeColor = Color.White;
            _btnCancelReservation.FlatStyle = FlatStyle.Flat;
            
            _pnlReservations.Controls.Add(_btnRefreshReservations);
            _pnlReservations.Controls.Add(_btnCancelReservation);
            
            // DataGridView Réservations
            _gridReservations.Dock = DockStyle.Fill;
            _gridReservations.AllowUserToResizeRows = false;
            _gridReservations.BackgroundColor = Color.White;
            _gridReservations.BorderStyle = BorderStyle.FixedSingle;
            _gridReservations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _gridReservations.ReadOnly = true;
            _gridReservations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            // ============================================
            // FORM
            // ============================================
            ClientSize = new Size(1000, 700);
            Controls.Add(_tabs);
            Controls.Add(_pnlNavigation);
            Text = "Client - Sport Reservation System";
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            
            _tabs.ResumeLayout(false);
            _tabTerrains.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_gridTerrains).EndInit();
            _tabCreneaux.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_gridCreneaux).EndInit();
            _pnlSearch.ResumeLayout(false);
            _tabReservations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_gridReservations).EndInit();
            _pnlReservations.ResumeLayout(false);
            _pnlNavigation.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}