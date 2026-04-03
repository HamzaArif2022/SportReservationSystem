using System.Drawing;

namespace SportReservationSystem.ClientApp
{
    partial class FormMainClient
    {
        private System.ComponentModel.IContainer components = null;

        // ── Palette Menthe Dorée ─────────────────────────────────────────
        //   Fond principal  #001a12   vert nuit profond
        //   Fond mid        #002a1a   légèrement plus clair
        //   Accent menthe   #059669   vert émeraude vif
        //   Menthe claire   #6ee7b7   titre / texte accentué
        //   Or              #fbbf24   bouton réserver + highlights
        //   Texte clair     #e6fff7   texte principal
        //   Texte atténué   #6ebca0   labels secondaires

        private static readonly Color MintBg = Color.FromArgb(0, 26, 18);
        private static readonly Color MintMid = Color.FromArgb(0, 42, 26);
        private static readonly Color MintAccent = Color.FromArgb(5, 150, 105);
        private static readonly Color MintLight = Color.FromArgb(110, 231, 183);
        private static readonly Color Gold = Color.FromArgb(251, 191, 36);
        private static readonly Color TextLight = Color.FromArgb(230, 255, 247);
        private static readonly Color TextMuted = Color.FromArgb(110, 188, 160);

        // ── Déclarations ─────────────────────────────────────────────────
        private MetroFramework.Controls.MetroTabControl _tabs;
        private MetroFramework.Controls.MetroTabPage _tabTerrains;
        private MetroFramework.Controls.MetroTabPage _tabCreneaux;
        private MetroFramework.Controls.MetroTabPage _tabReservations;
        private MetroFramework.Controls.MetroPanel _pnlNavigation;

        private MetroFramework.Controls.MetroGrid _gridTerrains;
        private MetroFramework.Controls.MetroLabel _lblTerrainsTitle;

        private MetroFramework.Controls.MetroGrid _gridCreneaux;
        private MetroFramework.Controls.MetroLabel _lblCreneauxTitle;
        private MetroFramework.Controls.MetroLabel _lblDateFilter;
        private MetroFramework.Controls.MetroPanel _pnlCreneauxFilter;
        private System.Windows.Forms.DateTimePicker _dateFilter;
        private MetroFramework.Controls.MetroButton _btnSearch;
        private MetroFramework.Controls.MetroButton _btnBook;

        private MetroFramework.Controls.MetroGrid _gridReservations;
        private MetroFramework.Controls.MetroLabel _lblReservationsTitle;
        private MetroFramework.Controls.MetroButton _btnRefreshReservations;
        private MetroFramework.Controls.MetroButton _btnCancelReservation;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // ════════════════════════════════════════════════════════════
            // FORM  — hérite de MetroFramework.Forms.MetroForm
            // ════════════════════════════════════════════════════════════
            this.Text = "Sport Réservation — Espace Client";
            this.ClientSize = new Size(1140, 740);
            this.MinimumSize = new Size(960, 640);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Resizable = true;
            this.BackColor = MintBg;

            // ════════════════════════════════════════════════════════════
            // NAVIGATION
            // ════════════════════════════════════════════════════════════
            _pnlNavigation = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(20, 62),
                Size = new Size(1100, 52),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            // ════════════════════════════════════════════════════════════
            // TABS
            // ════════════════════════════════════════════════════════════
            _tabs = new MetroFramework.Controls.MetroTabControl
            {
                Location = new Point(20, 124),
                Size = new Size(1100, 576),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroTabControlSize.Medium,
                BackColor = MintBg,
            };
            _tabs.SelectedIndexChanged += new System.EventHandler(this._tabs_SelectedIndexChanged);

            // ── Tab Terrains ─────────────────────────────────────────────
            _tabTerrains = new MetroFramework.Controls.MetroTabPage
            {
                Text = "  Terrains",
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintBg,
            };

            _lblTerrainsTitle = MakeLabel("Liste des terrains disponibles", new Point(8, 8), new Size(460, 28));
            _gridTerrains = MakeGrid(new Point(8, 44), new Size(1074, 490));

            _tabTerrains.Controls.Add(_lblTerrainsTitle);
            _tabTerrains.Controls.Add(_gridTerrains);

            // ── Tab Créneaux ─────────────────────────────────────────────
            _tabCreneaux = new MetroFramework.Controls.MetroTabPage
            {
                Text = "  Créneaux",
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintBg,
            };

            _lblCreneauxTitle = MakeLabel("Créneaux disponibles", new Point(8, 8), new Size(360, 28));

            _pnlCreneauxFilter = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(8, 44),
                Size = new Size(1074, 50),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            _lblDateFilter = new MetroFramework.Controls.MetroLabel
            {
                Text = "Date :",
                Location = new Point(10, 14),
                Size = new Size(52, 22),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = MintMid,
            };

            _dateFilter = new System.Windows.Forms.DateTimePicker
            {
                Location = new Point(68, 10),
                Size = new Size(165, 28),
                Format = System.Windows.Forms.DateTimePickerFormat.Short,
                Value = System.DateTime.Today,
                Font = new Font("Segoe UI", 10F),
                BackColor = MintMid,
                ForeColor = TextLight,
                CalendarMonthBackground = MintMid,
                CalendarForeColor = TextLight,
                CalendarTitleBackColor = MintAccent,
                CalendarTitleForeColor = TextLight,
            };

            _btnSearch = MakeButton("Rechercher", new Point(246, 8), new Size(132, 34));

            _btnBook = MakeButton("Réserver", new Point(390, 8), new Size(132, 34));
            _btnBook.Enabled = false;
            _btnBook.BackColor = Gold;
            _btnBook.ForeColor = Color.FromArgb(12, 12, 12);

            _pnlCreneauxFilter.Controls.Add(_lblDateFilter);
            _pnlCreneauxFilter.Controls.Add(_dateFilter);
            _pnlCreneauxFilter.Controls.Add(_btnSearch);
            _pnlCreneauxFilter.Controls.Add(_btnBook);

            _gridCreneaux = MakeGrid(new Point(8, 102), new Size(1074, 432));

            _tabCreneaux.Controls.Add(_lblCreneauxTitle);
            _tabCreneaux.Controls.Add(_pnlCreneauxFilter);
            _tabCreneaux.Controls.Add(_gridCreneaux);

            // ── Tab Réservations ─────────────────────────────────────────
            _tabReservations = new MetroFramework.Controls.MetroTabPage
            {
                Text = "  Mes Réservations",
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintBg,
            };

            _lblReservationsTitle = MakeLabel("Mes réservations", new Point(8, 8), new Size(360, 28));

            _btnRefreshReservations = MakeButton("Actualiser", new Point(840, 4), new Size(114, 34));

            _btnCancelReservation = MakeButton("Annuler", new Point(964, 4), new Size(114, 34));
            _btnCancelReservation.Enabled = false;
            _btnCancelReservation.BackColor = Color.FromArgb(180, 40, 40);
            _btnCancelReservation.ForeColor = TextLight;

            _gridReservations = MakeGrid(new Point(8, 46), new Size(1074, 488));

            _tabReservations.Controls.Add(_lblReservationsTitle);
            _tabReservations.Controls.Add(_btnRefreshReservations);
            _tabReservations.Controls.Add(_btnCancelReservation);
            _tabReservations.Controls.Add(_gridReservations);

            // ── Assemblage ───────────────────────────────────────────────
            _tabs.TabPages.Add(_tabTerrains);
            _tabs.TabPages.Add(_tabCreneaux);
            _tabs.TabPages.Add(_tabReservations);

            this.Controls.Add(_pnlNavigation);
            this.Controls.Add(_tabs);
        }

        // ── Helpers ──────────────────────────────────────────────────────

        private MetroFramework.Controls.MetroLabel MakeLabel(string text, Point loc, Size size)
            => new MetroFramework.Controls.MetroLabel
            {
                Text = text,
                Location = loc,
                Size = size,
                FontSize = MetroFramework.MetroLabelSize.Tall,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = MintLight,
                BackColor = MintBg,
            };

        private MetroFramework.Controls.MetroButton MakeButton(string text, Point loc, Size size)
            => new MetroFramework.Controls.MetroButton
            {
                Text = text,
                Location = loc,
                Size = size,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroButtonSize.Medium,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

        private MetroFramework.Controls.MetroGrid MakeGrid(Point loc, Size size)
        {
            var g = new MetroFramework.Controls.MetroGrid
            {
                Location = loc,
                Size = size,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = MintBg,
                GridColor = MintMid,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                ColumnHeadersHeight = 38,
                EnableHeadersVisualStyles = false,
                RowTemplate = { Height = 32 },
            };

            g.ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor = MintMid,
                ForeColor = MintLight,
                SelectionBackColor = MintAccent,
                SelectionForeColor = TextLight,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            };
            g.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor = MintBg,
                ForeColor = TextLight,
                SelectionBackColor = MintAccent,
                SelectionForeColor = TextLight,
                Font = new Font("Segoe UI", 9F),
            };
            g.AlternatingRowsDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
            {
                BackColor = MintMid,
                ForeColor = TextLight,
                SelectionBackColor = MintAccent,
                SelectionForeColor = TextLight,
            };
            return g;
        }
    }
}