using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.GestionnaireApp.Forms
{
    partial class FormValidationReservations
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
        private static readonly Color DangerRed = Color.FromArgb(200, 50, 50);

        // ── Contrôles ────────────────────────────────────────────────────
        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblCount;
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroPanel pnlTop;
        private MetroFramework.Controls.MetroPanel pnlBottom;
        private MetroFramework.Controls.MetroButton btnValider;
        private MetroFramework.Controls.MetroButton btnRefuser;
        private MetroFramework.Controls.MetroButton btnRafraichir;
        private MetroFramework.Controls.MetroGrid dataGridViewReservations;

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
            this.Text = "Validations — Sport Réservation";
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
                Text = "✔  Validation des Réservations",
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

            // ── Panel Top (compteur) ─────────────────────────────────────
            pnlTop = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(0, 70),
                Size = new Size(1060, 54),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelMid,
            };

            lblCount = new MetroFramework.Controls.MetroLabel
            {
                Text = "Chargement...",
                Location = new Point(20, 16),
                Size = new Size(600, 26),
                FontSize = MetroFramework.MetroLabelSize.Medium,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = Gold,
                BackColor = SteelMid,
            };

            pnlTop.Controls.Add(lblCount);

            // ── Séparateur ───────────────────────────────────────────────
            lblDivider = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(0, 124),
                Size = new Size(1060, 2),
                BackColor = SteelAccent,
                ForeColor = SteelAccent,
            };

            // ── Grille ───────────────────────────────────────────────────
            dataGridViewReservations = new MetroFramework.Controls.MetroGrid
            {
                Location = new Point(20, 136),
                Size = new Size(1020, 400),
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

            // ── Panel Bottom (boutons) ───────────────────────────────────
            pnlBottom = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(0, 546),
                Size = new Size(1060, 54),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelMid,
            };

            btnValider = new MetroFramework.Controls.MetroButton
            {
                Text = "✔  Valider",
                Location = new Point(20, 12),
                Size = new Size(150, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SuccessGreen,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            btnRefuser = new MetroFramework.Controls.MetroButton
            {
                Text = "✖  Refuser",
                Location = new Point(182, 12),
                Size = new Size(150, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = DangerRed,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            btnRafraichir = new MetroFramework.Controls.MetroButton
            {
                Text = "🔄  Rafraîchir",
                Location = new Point(888, 12),
                Size = new Size(152, 32),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelAccent,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            pnlBottom.Controls.Add(btnValider);
            pnlBottom.Controls.Add(btnRefuser);
            pnlBottom.Controls.Add(btnRafraichir);

            // ── Assemblage ──────────────────────────────────────────────
            this.Controls.Add(dataGridViewReservations);
            this.Controls.Add(lblDivider);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(pnlTop);
            this.Controls.Add(lblTitle);
        }
    }
}