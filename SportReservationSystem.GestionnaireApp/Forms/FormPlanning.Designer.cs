using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.GestionnaireApp.Forms
{
    partial class FormPlanning
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

        // ── Contrôles ────────────────────────────────────────────────────
        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblDateLabel;
        private MetroFramework.Controls.MetroLabel lblDate;
        private MetroFramework.Controls.MetroLabel lblCount;
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroPanel pnlTop;
        private MetroFramework.Controls.MetroGrid dataGridViewPlanning;
        private System.Windows.Forms.DateTimePicker dtpDate;

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
            this.Text = "Planning — Sport Réservation";
            this.ClientSize = new Size(1060, 620);
            this.MinimumSize = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.BackColor = SteelBg;

            // ── Titre ───────────────────────────────────────────────────
            lblTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "📅  Planning des Réservations",
                Location = new Point(0, 32),
                Size = new Size(1060, 32),
                FontSize = MetroFramework.MetroLabelSize.Medium,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = SteelLight,
                BackColor = SteelBg,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            // ── Panel Top ────────────────────────────────────────────────
            pnlTop = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(0, 70),
                Size = new Size(1060, 64),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelMid,
            };

            // ── Label "Date :" ───────────────────────────────────────────
            lblDateLabel = new MetroFramework.Controls.MetroLabel
            {
                Text = "Date :",
                Location = new Point(20, 22),
                Size = new Size(50, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = SteelMid,
            };

            // ── DateTimePicker ───────────────────────────────────────────
            dtpDate = new DateTimePicker
            {
                Location = new Point(76, 19),
                Size = new Size(200, 28),
                Format = DateTimePickerFormat.Short,
                CalendarForeColor = TextLight,
                CalendarMonthBackground = SteelMid,
                CalendarTitleBackColor = SteelAccent,
                CalendarTitleForeColor = TextLight,
                BackColor = SteelBg,
                ForeColor = TextLight,
            };

            // ── Label date sélectionnée ──────────────────────────────────
            lblDate = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(294, 20),
                Size = new Size(460, 26),
                FontSize = MetroFramework.MetroLabelSize.Medium,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = Gold,
                BackColor = SteelMid,
            };

            // ── Label compteur ───────────────────────────────────────────
            lblCount = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(800, 22),
                Size = new Size(240, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = SteelMid,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
            };

            pnlTop.Controls.Add(lblDateLabel);
            pnlTop.Controls.Add(dtpDate);
            pnlTop.Controls.Add(lblDate);
            pnlTop.Controls.Add(lblCount);

            // ── Séparateur ───────────────────────────────────────────────
            lblDivider = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(0, 134),
                Size = new Size(1060, 2),
                BackColor = SteelAccent,
                ForeColor = SteelAccent,
            };

            // ── Grille ───────────────────────────────────────────────────
            dataGridViewPlanning = new MetroFramework.Controls.MetroGrid
            {
                Location = new Point(20, 146),
                Size = new Size(1020, 440),
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
                    BackColor = Color.FromArgb(14, 28, 52),
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

            // ── Assemblage ──────────────────────────────────────────────
            this.Controls.Add(dataGridViewPlanning);
            this.Controls.Add(lblDivider);
            this.Controls.Add(pnlTop);
            this.Controls.Add(lblTitle);
        }
    }
}