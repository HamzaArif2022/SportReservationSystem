using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.GestionnaireApp.Forms
{
    partial class FormStatistiques
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
        private static readonly Color SuccessGreen = Color.FromArgb(34, 180, 100);
        private static readonly Color CardBg = Color.FromArgb(14, 28, 52);

        // ── Contrôles ────────────────────────────────────────────────────
        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroTabControl tabControl;
        private MetroFramework.Controls.MetroTabPage tabDashboard;
        private MetroFramework.Controls.MetroTabPage tabCA;

        // Tab Dashboard — KPI Cards
        private MetroFramework.Controls.MetroPanel pnlKpi;
        private MetroFramework.Controls.MetroPanel cardAujourdHui;
        private MetroFramework.Controls.MetroPanel cardEnAttente;
        private MetroFramework.Controls.MetroPanel cardConfirmees;
        private MetroFramework.Controls.MetroPanel cardCAMois;
        private MetroFramework.Controls.MetroPanel cardOccupation;
        private MetroFramework.Controls.MetroLabel lblAujourdHuiTitle;
        private MetroFramework.Controls.MetroLabel lblAujourdHui;
        private MetroFramework.Controls.MetroLabel lblEnAttenteTitle;
        private MetroFramework.Controls.MetroLabel lblEnAttente;
        private MetroFramework.Controls.MetroLabel lblConfirmeesTitle;
        private MetroFramework.Controls.MetroLabel lblConfirmees;
        private MetroFramework.Controls.MetroLabel lblCAMoisTitle;
        private MetroFramework.Controls.MetroLabel lblCAMois;
        private MetroFramework.Controls.MetroLabel lblOccupationTitle;
        private MetroFramework.Controls.MetroLabel lblOccupation;

        // Tab Dashboard — Grilles
        private MetroFramework.Controls.MetroLabel lblDernieresTitle;
        private MetroFramework.Controls.MetroLabel lblTopTerrainsTitle;
        private MetroFramework.Controls.MetroGrid dataGridViewDernieres;
        private MetroFramework.Controls.MetroGrid dataGridViewTopTerrains;

        // Tab CA
        private MetroFramework.Controls.MetroPanel pnlCATop;
        private MetroFramework.Controls.MetroLabel lblPeriodeLabel;
        private MetroFramework.Controls.MetroComboBox cmbPeriode;
        private MetroFramework.Controls.MetroPanel cardCATotal;
        private MetroFramework.Controls.MetroPanel cardCAEvolution;
        private MetroFramework.Controls.MetroLabel lblCATotalTitle;
        private MetroFramework.Controls.MetroLabel lblCATotal;
        private MetroFramework.Controls.MetroLabel lblCAEvolutionTitle;
        private MetroFramework.Controls.MetroLabel lblCAEvolution;
        private MetroFramework.Controls.MetroLabel lblParMoisTitle;
        private MetroFramework.Controls.MetroLabel lblParTerrainTitle;
        private MetroFramework.Controls.MetroGrid dataGridViewCAMois;
        private MetroFramework.Controls.MetroGrid dataGridViewCATerrain;
        private MetroFramework.Controls.MetroButton btnExporter;

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
            this.Text = "Statistiques — Sport Réservation";
            this.ClientSize = new Size(1160, 720);
            this.MinimumSize = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.BackColor = SteelBg;

            // ── Titre ───────────────────────────────────────────────────
            lblTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "📊  Statistiques & Tableau de Bord",
                Location = new Point(0, 32),
                Size = new Size(1160, 32),
                FontSize = MetroFramework.MetroLabelSize.Medium,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = SteelLight,
                BackColor = SteelBg,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            lblDivider = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(0, 68),
                Size = new Size(1160, 2),
                BackColor = SteelAccent,
                ForeColor = SteelAccent,
            };

            // ════════════════════════════════════════════════════════════
            // TABS
            // ════════════════════════════════════════════════════════════
            tabControl = new MetroFramework.Controls.MetroTabControl
            {
                Location = new Point(0, 72),
                Size = new Size(1160, 648),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroTabControlSize.Medium,
                BackColor = SteelBg,
            };

            // ════════════════════════════════════════════════════════════
            // TAB DASHBOARD
            // ════════════════════════════════════════════════════════════
            tabDashboard = new MetroFramework.Controls.MetroTabPage
            {
                Text = "Dashboard",
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelBg,
            };

            // ── KPI Panel ────────────────────────────────────────────────
            pnlKpi = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(10, 10),
                Size = new Size(1120, 110),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelBg,
            };

            // helper local pour créer une carte KPI
            MetroFramework.Controls.MetroPanel MakeCard(int x, string emoji, Color accent) =>
                new MetroFramework.Controls.MetroPanel
                {
                    Location = new Point(x, 0),
                    Size = new Size(208, 106),
                    Style = MetroFramework.MetroColorStyle.Blue,
                    Theme = MetroFramework.MetroThemeStyle.Dark,
                    BackColor = CardBg,
                };

            MetroFramework.Controls.MetroLabel MakeCardTitle(string text, Color color) =>
                new MetroFramework.Controls.MetroLabel
                {
                    Text = text,
                    Location = new Point(12, 10),
                    Size = new Size(184, 20),
                    FontSize = MetroFramework.MetroLabelSize.Small,
                    FontWeight = MetroFramework.MetroLabelWeight.Bold,
                    Style = MetroFramework.MetroColorStyle.Blue,
                    Theme = MetroFramework.MetroThemeStyle.Dark,
                    ForeColor = color,
                    BackColor = CardBg,
                };

            MetroFramework.Controls.MetroLabel MakeCardValue() =>
                new MetroFramework.Controls.MetroLabel
                {
                    Text = "—",
                    Location = new Point(12, 40),
                    Size = new Size(184, 52),
                    FontSize = MetroFramework.MetroLabelSize.Tall,
                    FontWeight = MetroFramework.MetroLabelWeight.Bold,
                    Style = MetroFramework.MetroColorStyle.Blue,
                    Theme = MetroFramework.MetroThemeStyle.Dark,
                    ForeColor = TextLight,
                    BackColor = CardBg,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                };

            // Carte Aujourd'hui
            cardAujourdHui = MakeCard(0, "📅", SteelAccent);
            lblAujourdHuiTitle = MakeCardTitle("📅  Aujourd'hui", SteelLight);
            lblAujourdHui = MakeCardValue();
            cardAujourdHui.Controls.Add(lblAujourdHuiTitle);
            cardAujourdHui.Controls.Add(lblAujourdHui);

            // Carte En attente
            cardEnAttente = MakeCard(220, "⏳", Gold);
            lblEnAttenteTitle = MakeCardTitle("⏳  En attente", Gold);
            lblEnAttente = MakeCardValue();
            cardEnAttente.Controls.Add(lblEnAttenteTitle);
            cardEnAttente.Controls.Add(lblEnAttente);

            // Carte Confirmées
            cardConfirmees = MakeCard(440, "✔", SuccessGreen);
            lblConfirmeesTitle = MakeCardTitle("✔  Confirmées", SuccessGreen);
            lblConfirmees = MakeCardValue();
            cardConfirmees.Controls.Add(lblConfirmeesTitle);
            cardConfirmees.Controls.Add(lblConfirmees);

            // Carte CA Mois
            cardCAMois = MakeCard(660, "💰", Gold);
            lblCAMoisTitle = MakeCardTitle("💰  CA du mois", Gold);
            lblCAMois = MakeCardValue();
            cardCAMois.Controls.Add(lblCAMoisTitle);
            cardCAMois.Controls.Add(lblCAMois);

            // Carte Occupation
            cardOccupation = MakeCard(880, "📈", SteelLight);
            lblOccupationTitle = MakeCardTitle("📈  Taux d'occupation", SteelLight);
            lblOccupation = MakeCardValue();
            cardOccupation.Controls.Add(lblOccupationTitle);
            cardOccupation.Controls.Add(lblOccupation);

            pnlKpi.Controls.Add(cardAujourdHui);
            pnlKpi.Controls.Add(cardEnAttente);
            pnlKpi.Controls.Add(cardConfirmees);
            pnlKpi.Controls.Add(cardCAMois);
            pnlKpi.Controls.Add(cardOccupation);

            // ── Grille Dernières réservations ────────────────────────────
            lblDernieresTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Dernières réservations",
                Location = new Point(10, 128),
                Size = new Size(540, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = SteelBg,
            };

            dataGridViewDernieres = BuildGrid();
            dataGridViewDernieres.Location = new Point(10, 154);
            dataGridViewDernieres.Size = new Size(540, 430);

            // ── Grille Top Terrains ──────────────────────────────────────
            lblTopTerrainsTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Top terrains",
                Location = new Point(570, 128),
                Size = new Size(560, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = SteelBg,
            };

            dataGridViewTopTerrains = BuildGrid();
            dataGridViewTopTerrains.Location = new Point(570, 154);
            dataGridViewTopTerrains.Size = new Size(560, 430);

            tabDashboard.Controls.Add(pnlKpi);
            tabDashboard.Controls.Add(lblDernieresTitle);
            tabDashboard.Controls.Add(dataGridViewDernieres);
            tabDashboard.Controls.Add(lblTopTerrainsTitle);
            tabDashboard.Controls.Add(dataGridViewTopTerrains);

            // ════════════════════════════════════════════════════════════
            // TAB CHIFFRE D'AFFAIRES
            // ════════════════════════════════════════════════════════════
            tabCA = new MetroFramework.Controls.MetroTabPage
            {
                Text = "Chiffre d'Affaires",
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelBg,
            };

            // ── Panel top CA ─────────────────────────────────────────────
            pnlCATop = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(10, 10),
                Size = new Size(1120, 110),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelBg,
            };

            // Période
            lblPeriodeLabel = new MetroFramework.Controls.MetroLabel
            {
                Text = "Période :",
                Location = new Point(0, 16),
                Size = new Size(80, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = SteelBg,
            };

            cmbPeriode = new MetroFramework.Controls.MetroComboBox
            {
                Location = new Point(86, 10),
                Size = new Size(160, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelMid,
                ForeColor = TextLight,
                FormattingEnabled = true,
                UseSelectable = true,
            };
            cmbPeriode.Items.AddRange(new object[] { "Semaine", "Mois", "Année" });

            // Carte CA Total
            cardCATotal = MakeCard(300, "💰", Gold);
            cardCATotal.Size = new Size(240, 106);
            lblCATotalTitle = MakeCardTitle("💰  CA Total", Gold);
            lblCATotal = MakeCardValue();
            cardCATotal.Controls.Add(lblCATotalTitle);
            cardCATotal.Controls.Add(lblCATotal);

            // Carte Evolution
            cardCAEvolution = MakeCard(556, "📈", SteelLight);
            cardCAEvolution.Size = new Size(240, 106);
            lblCAEvolutionTitle = MakeCardTitle("📈  Évolution", SteelLight);
            lblCAEvolution = MakeCardValue();
            cardCAEvolution.Controls.Add(lblCAEvolutionTitle);
            cardCAEvolution.Controls.Add(lblCAEvolution);

            pnlCATop.Controls.Add(lblPeriodeLabel);
            pnlCATop.Controls.Add(cmbPeriode);
            pnlCATop.Controls.Add(cardCATotal);
            pnlCATop.Controls.Add(cardCAEvolution);

            // ── Grille Par mois ──────────────────────────────────────────
            lblParMoisTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Répartition par mois",
                Location = new Point(10, 128),
                Size = new Size(540, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = SteelBg,
            };

            dataGridViewCAMois = BuildGrid();
            dataGridViewCAMois.Location = new Point(10, 154);
            dataGridViewCAMois.Size = new Size(540, 380);

            // ── Grille Par terrain ───────────────────────────────────────
            lblParTerrainTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Répartition par terrain",
                Location = new Point(570, 128),
                Size = new Size(560, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = SteelBg,
            };

            dataGridViewCATerrain = BuildGrid();
            dataGridViewCATerrain.Location = new Point(570, 154);
            dataGridViewCATerrain.Size = new Size(560, 380);

            // ── Bouton Exporter ──────────────────────────────────────────
            btnExporter = new MetroFramework.Controls.MetroButton
            {
                Text = "📥  Exporter rapport",
                Location = new Point(960, 548),
                Size = new Size(170, 36),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = Gold,
                ForeColor = Color.FromArgb(12, 12, 12),
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            tabCA.Controls.Add(pnlCATop);
            tabCA.Controls.Add(lblParMoisTitle);
            tabCA.Controls.Add(dataGridViewCAMois);
            tabCA.Controls.Add(lblParTerrainTitle);
            tabCA.Controls.Add(dataGridViewCATerrain);
            tabCA.Controls.Add(btnExporter);

            // ── Assemblage tabs ──────────────────────────────────────────
            tabControl.Controls.Add(tabDashboard);
            tabControl.Controls.Add(tabCA);

            this.Controls.Add(tabControl);
            this.Controls.Add(lblDivider);
            this.Controls.Add(lblTitle);
        }

        // ── Helper grille ────────────────────────────────────────────────
        private MetroFramework.Controls.MetroGrid BuildGrid() =>
            new MetroFramework.Controls.MetroGrid
            {
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackgroundColor = SteelBg,
                ForeColor = TextLight,
                GridColor = SteelAccent,
                RowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = SteelMid,
                    ForeColor = TextLight,
                    SelectionBackColor = SteelAccent,
                    SelectionForeColor = TextLight,
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = CardBg,
                    ForeColor = TextLight,
                    SelectionBackColor = SteelAccent,
                    SelectionForeColor = TextLight,
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
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
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            };
    }
}