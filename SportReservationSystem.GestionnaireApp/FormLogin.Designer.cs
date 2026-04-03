using System.Drawing;

namespace SportReservationSystem.GestionnaireApp
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        // ── Palette Acier Doré (Gestionnaire) ───────────────────────────
        private static readonly Color SteelBg = Color.FromArgb(10, 20, 40);
        private static readonly Color SteelMid = Color.FromArgb(18, 35, 65);
        private static readonly Color SteelAccent = Color.FromArgb(30, 90, 180);
        private static readonly Color SteelLight = Color.FromArgb(120, 180, 255);
        private static readonly Color Gold = Color.FromArgb(251, 191, 36);
        private static readonly Color TextLight = Color.FromArgb(225, 235, 255);
        private static readonly Color TextMuted = Color.FromArgb(100, 140, 200);

        // ── Contrôles ────────────────────────────────────────────────────
        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblSubtitle;
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroLabel lblEmail;
        private MetroFramework.Controls.MetroLabel lblPassword;
        private MetroFramework.Controls.MetroTextBox txtEmail;
        private MetroFramework.Controls.MetroTextBox txtPassword;
        private MetroFramework.Controls.MetroButton btnLogin;
        private MetroFramework.Controls.MetroPanel pnlCard;

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
            this.ClientSize = new Size(440, 440);
            this.MinimumSize = new Size(440, 440);
            this.MaximumSize = new Size(440, 440);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Style = MetroFramework.MetroColorStyle.Blue;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Resizable = false;
            this.BackColor = SteelBg;

            // ── CARTE ───────────────────────────────────────────────────
            pnlCard = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(30, 70),
                Size = new Size(380, 335),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = SteelMid,
            };

            // ── Titre ───────────────────────────────────────────────────
            lblTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "⚙️ Espace Gestionnaire",
                Location = new Point(0, 28),
                Size = new Size(380, 38),
                FontSize = MetroFramework.MetroLabelSize.Tall,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = SteelLight,
                BackColor = SteelMid,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            // ── Sous-titre ──────────────────────────────────────────────
            lblSubtitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Connectez-vous à votre espace de gestion",
                Location = new Point(0, 70),
                Size = new Size(380, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = SteelMid,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            // ── Séparateur ──────────────────────────────────────────────
            lblDivider = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(30, 102),
                Size = new Size(320, 2),
                BackColor = SteelAccent,
                ForeColor = SteelAccent,
            };

            // ── Label Email ─────────────────────────────────────────────
            lblEmail = new MetroFramework.Controls.MetroLabel
            {
                Text = "Adresse e-mail",
                Location = new Point(30, 120),
                Size = new Size(320, 20),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = SteelMid,
            };

            // ── TextBox Email ───────────────────────────────────────────
            txtEmail = new MetroFramework.Controls.MetroTextBox
            {
                Location = new Point(30, 144),
                Size = new Size(320, 34),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroTextBoxSize.Medium,
                BackColor = SteelBg,
                ForeColor = TextLight,
                WaterMark = "gestionnaire@email.com",
                WaterMarkColor = TextMuted,
                UseSelectable = true,
            };

            // ── Label Mot de passe ──────────────────────────────────────
            lblPassword = new MetroFramework.Controls.MetroLabel
            {
                Text = "Mot de passe",
                Location = new Point(30, 192),
                Size = new Size(320, 20),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = SteelMid,
            };

            // ── TextBox Mot de passe ────────────────────────────────────
            txtPassword = new MetroFramework.Controls.MetroTextBox
            {
                Location = new Point(30, 216),
                Size = new Size(320, 34),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroTextBoxSize.Medium,
                BackColor = SteelBg,
                ForeColor = TextLight,
                PasswordChar = '●',
                WaterMark = "••••••••",
                WaterMarkColor = TextMuted,
                UseSelectable = true,
            };

            // ── Bouton Se connecter ─────────────────────────────────────
            btnLogin = new MetroFramework.Controls.MetroButton
            {
                Text = "Accéder au tableau de bord",
                Location = new Point(30, 278),
                Size = new Size(320, 40),
                Style = MetroFramework.MetroColorStyle.Blue,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroButtonSize.Medium,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                BackColor = Gold,
                ForeColor = System.Drawing.Color.FromArgb(12, 12, 12),
                UseSelectable = true,
            };

            // ── Assemblage ──────────────────────────────────────────────
            pnlCard.Controls.Add(lblTitle);
            pnlCard.Controls.Add(lblSubtitle);
            pnlCard.Controls.Add(lblDivider);
            pnlCard.Controls.Add(lblEmail);
            pnlCard.Controls.Add(txtEmail);
            pnlCard.Controls.Add(lblPassword);
            pnlCard.Controls.Add(txtPassword);
            pnlCard.Controls.Add(btnLogin);

            this.Controls.Add(pnlCard);
        }
    }
}