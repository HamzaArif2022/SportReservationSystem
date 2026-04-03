using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.ClientApp.Forms
{
    partial class FormHistorique
    {
        private System.ComponentModel.IContainer components = null;

        // ── Palette Menthe Dorée ─────────────────────────────────────────
        private static readonly Color MintBg = Color.FromArgb(0, 26, 18);
        private static readonly Color MintMid = Color.FromArgb(0, 42, 26);
        private static readonly Color MintAccent = Color.FromArgb(5, 150, 105);
        private static readonly Color MintLight = Color.FromArgb(110, 231, 183);
        private static readonly Color Gold = Color.FromArgb(251, 191, 36);
        private static readonly Color TextLight = Color.FromArgb(230, 255, 247);
        private static readonly Color TextMuted = Color.FromArgb(110, 188, 160);
        private static readonly Color DangerRed = Color.FromArgb(220, 50, 50);
        private static readonly Color CardBg = Color.FromArgb(0, 34, 22);

        // ── Contrôles ────────────────────────────────────────────────────
        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroLabel lblStatus;
        private MetroFramework.Controls.MetroPanel pnlBottom;
        private MetroFramework.Controls.MetroButton btnAnnuler;
        private MetroFramework.Controls.MetroGrid dataGridViewHistorique;

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
            this.Text = "Mon Historique — Sport Réservation";
            this.ClientSize = new Size(900, 560);
            this.MinimumSize = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.BackColor = MintBg;

            // ── Titre ───────────────────────────────────────────────────
            lblTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "📋  Historique des Réservations",
                Location = new Point(0, 32),
                Size = new Size(900, 32),
                FontSize = MetroFramework.MetroLabelSize.Medium,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = MintLight,
                BackColor = MintBg,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            // ── Séparateur ───────────────────────────────────────────────
            lblDivider = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(0, 68),
                Size = new Size(900, 2),
                BackColor = MintAccent,
                ForeColor = MintAccent,
            };

            // ── Grille ───────────────────────────────────────────────────
            dataGridViewHistorique = new MetroFramework.Controls.MetroGrid
            {
                Location = new Point(20, 82),
                Size = new Size(860, 410),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackgroundColor = MintBg,
                ForeColor = TextLight,
                GridColor = MintAccent,
                RowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = MintMid,
                    ForeColor = TextLight,
                    SelectionBackColor = MintAccent,
                    SelectionForeColor = TextLight,
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = CardBg,
                    ForeColor = TextLight,
                    SelectionBackColor = MintAccent,
                    SelectionForeColor = TextLight,
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = MintAccent,
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

            // ── Panel Bottom ─────────────────────────────────────────────
            pnlBottom = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(0, 502),
                Size = new Size(900, 54),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            lblStatus = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(20, 17),
                Size = new Size(500, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = MintMid,
            };

            btnAnnuler = new MetroFramework.Controls.MetroButton
            {
                Text = "✖  Annuler la réservation",
                Location = new Point(706, 12),
                Size = new Size(174, 32),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = DangerRed,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            pnlBottom.Controls.Add(lblStatus);
            pnlBottom.Controls.Add(btnAnnuler);

            // ── Assemblage ──────────────────────────────────────────────
            this.Controls.Add(dataGridViewHistorique);
            this.Controls.Add(lblDivider);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(lblTitle);
        }
    }
}