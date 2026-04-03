using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.GestionnaireApp
{
    partial class FormMainGestionnaire
    {
        private System.ComponentModel.IContainer components = null;

        // ── Palette Acier Doré ───────────────────────────────────────────
        private static readonly Color SteelBg = Color.FromArgb(10, 20, 40);
        private static readonly Color SteelMid = Color.FromArgb(18, 35, 65);
        private static readonly Color SteelAccent = Color.FromArgb(30, 90, 180);
        private static readonly Color SteelLight = Color.FromArgb(120, 180, 255);
        private static readonly Color Gold = Color.FromArgb(251, 191, 36);
        private static readonly Color TextLight = Color.FromArgb(225, 235, 255);
        private static readonly Color TextMuted = Color.FromArgb(100, 140, 200);
        private static readonly Color DangerRed = Color.FromArgb(220, 50, 50);
        private static readonly Color SuccessGreen = Color.FromArgb(34, 180, 100);

        // ── Contrôles ────────────────────────────────────────────────────
        private MetroFramework.Controls.MetroTabControl _tabs;
        private MetroFramework.Controls.MetroTabPage _tabDashboard;
        private MetroFramework.Controls.MetroTabPage _tabValidations;
        private MetroFramework.Controls.MetroPanel _pnlTop;
        private MetroFramework.Controls.MetroPanel _pnlDashboard;
        private MetroFramework.Controls.MetroPanel _pnlValidations;
        private MetroFramework.Controls.MetroGrid _gridDashboard;
        private MetroFramework.Controls.MetroGrid _gridValidations;
        private MetroFramework.Controls.MetroButton _btnRefreshDashboard;
        private MetroFramework.Controls.MetroButton _btnRefresh;
        private MetroFramework.Controls.MetroButton _btnValidate;
        private MetroFramework.Controls.MetroButton _btnCancel;
        private MetroFramework.Controls.MetroButton _btnValidation;
        private MetroFramework.Controls.MetroButton _btnPlanning;
        private MetroFramework.Controls.MetroButton _btnStatistiques;
        private MetroFramework.Controls.MetroLabel _lblHeader;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // ── FORM ────────────────────────────────────────────────────
            this.Text = "Gestionnaire — Sport Réservation";
            this.ClientSize = new Size(1100, 720);
            this.MinimumSize = new Size(1000, 650);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.BackColor = SteelBg;

            // ── HEADER ──────────────────────────────────────────────────
            _lblHeader = new MetroFramework.Controls.MetroLabel
            {
                Text = "⚙️  Sport Réservation — Tableau de Bord Gestionnaire",
                Location = new Point(0, 32),
                Size = new Size(1100, 32),
                FontSize = MetroFramework.MetroLabelSize.Medium,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = SteelLight,
                BackColor = SteelBg,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            // ── PANEL TOP (Navigation rapide) ───────────────────────────
            _pnlTop = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(0, 70),
                Size = new Size(1100, 54),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelMid,
            };

            _btnValidation = new MetroFramework.Controls.MetroButton
            {
                Text = "✔ Validations",
                Location = new Point(20, 12),
                Size = new Size(150, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelAccent,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            _btnPlanning = new MetroFramework.Controls.MetroButton
            {
                Text = "📅 Planning",
                Location = new Point(180, 12),
                Size = new Size(150, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelAccent,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            _btnStatistiques = new MetroFramework.Controls.MetroButton
            {
                Text = "📊 Statistiques",
                Location = new Point(340, 12),
                Size = new Size(150, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelAccent,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            _pnlTop.Controls.Add(_btnValidation);
            _pnlTop.Controls.Add(_btnPlanning);
            _pnlTop.Controls.Add(_btnStatistiques);

            // ── TABS ────────────────────────────────────────────────────
            _tabs = new MetroFramework.Controls.MetroTabControl
            {
                Location = new Point(0, 124),
                Size = new Size(1100, 596),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroTabControlSize.Medium,
                BackColor = SteelBg,
            };

            // ── Tab Dashboard ────────────────────────────────────────────
            _tabDashboard = new MetroFramework.Controls.MetroTabPage
            {
                Text = "Dashboard / Planning",
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelBg,
            };

            _pnlDashboard = new MetroFramework.Controls.MetroPanel
            {
                Dock = DockStyle.Top,
                Height = 54,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelMid,
            };

            _btnRefreshDashboard = new MetroFramework.Controls.MetroButton
            {
                Text = "🔄 Actualiser le planning",
                Location = new Point(16, 12),
                Size = new Size(200, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = Gold,
                ForeColor = Color.FromArgb(12, 12, 12),
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            _pnlDashboard.Controls.Add(_btnRefreshDashboard);

            _gridDashboard = new MetroFramework.Controls.MetroGrid
            {
                Dock = DockStyle.Fill,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackgroundColor = SteelBg,
                ForeColor = TextLight,
                GridColor = SteelAccent,
                RowsDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
                {
                    BackColor = SteelMid,
                    ForeColor = TextLight,
                    SelectionBackColor = SteelAccent,
                    SelectionForeColor = TextLight,
                },
                ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
                {
                    BackColor = SteelAccent,
                    ForeColor = TextLight,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                },
                EnableHeadersVisualStyles = false,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill,
            };

            _tabDashboard.Controls.Add(_gridDashboard);
            _tabDashboard.Controls.Add(_pnlDashboard);

            // ── Tab Validations ──────────────────────────────────────────
            _tabValidations = new MetroFramework.Controls.MetroTabPage
            {
                Text = "Validations / Annulations",
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelBg,
            };

            _pnlValidations = new MetroFramework.Controls.MetroPanel
            {
                Dock = DockStyle.Top,
                Height = 54,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelMid,
            };

            _btnRefresh = new MetroFramework.Controls.MetroButton
            {
                Text = "🔄 Actualiser",
                Location = new Point(16, 12),
                Size = new Size(140, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelAccent,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            _btnValidate = new MetroFramework.Controls.MetroButton
            {
                Text = "✔ Valider",
                Location = new Point(168, 12),
                Size = new Size(140, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SuccessGreen,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            _btnCancel = new MetroFramework.Controls.MetroButton
            {
                Text = "✖ Annuler",
                Location = new Point(320, 12),
                Size = new Size(140, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = DangerRed,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            _pnlValidations.Controls.Add(_btnRefresh);
            _pnlValidations.Controls.Add(_btnValidate);
            _pnlValidations.Controls.Add(_btnCancel);

            _gridValidations = new MetroFramework.Controls.MetroGrid
            {
                Dock = DockStyle.Fill,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackgroundColor = SteelBg,
                ForeColor = TextLight,
                GridColor = SteelAccent,
                RowsDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
                {
                    BackColor = SteelMid,
                    ForeColor = TextLight,
                    SelectionBackColor = SteelAccent,
                    SelectionForeColor = TextLight,
                },
                ColumnHeadersDefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle
                {
                    BackColor = SteelAccent,
                    ForeColor = TextLight,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                },
                EnableHeadersVisualStyles = false,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill,
            };

            _tabValidations.Controls.Add(_gridValidations);
            _tabValidations.Controls.Add(_pnlValidations);

            // ── Assemblage ──────────────────────────────────────────────
            _tabs.Controls.Add(_tabDashboard);
            _tabs.Controls.Add(_tabValidations);

            this.Controls.Add(_tabs);
            this.Controls.Add(_pnlTop);
            this.Controls.Add(_lblHeader);
        }
    }
}