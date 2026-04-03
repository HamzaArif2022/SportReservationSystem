using System.Drawing;

namespace SportReservationSystem.ClientApp
{
    partial class FormLogin
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

        // ── Contrôles ────────────────────────────────────────────────────
        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblEmail;
        private MetroFramework.Controls.MetroLabel lblPassword;
        private MetroFramework.Controls.MetroLabel lblSubtitle;
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroTextBox txtEmail;
        private MetroFramework.Controls.MetroTextBox txtPassword;
        private MetroFramework.Controls.MetroButton btnLogin;
        private MetroFramework.Controls.MetroButton btnRegister;
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
            this.Text = "Connexion — Sport Réservation";
            this.ClientSize = new Size(440, 480);
            this.MinimumSize = new Size(440, 480);
            this.MaximumSize = new Size(440, 480);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Resizable = false;
            this.BackColor = MintBg;

            // ── CARTE ───────────────────────────────────────────────────
            pnlCard = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(30, 70),
                Size = new Size(380, 370),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            // ── Titre ───────────────────────────────────────────────────
            lblTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "🏅 Sport Réservation",
                Location = new Point(0, 28),
                Size = new Size(380, 38),
                FontSize = MetroFramework.MetroLabelSize.Tall,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = MintLight,
                BackColor = MintMid,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            // ── Sous-titre ──────────────────────────────────────────────
            lblSubtitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Connectez-vous à votre espace client",
                Location = new Point(0, 70),
                Size = new Size(380, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = MintMid,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            // ── Séparateur ──────────────────────────────────────────────
            lblDivider = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(30, 102),
                Size = new Size(320, 2),
                BackColor = MintAccent,
                ForeColor = MintAccent,
            };

            // ── Label Email ─────────────────────────────────────────────
            lblEmail = new MetroFramework.Controls.MetroLabel
            {
                Text = "Adresse e-mail",
                Location = new Point(30, 120),
                Size = new Size(320, 20),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = MintMid,
            };

            // ── TextBox Email ───────────────────────────────────────────
            txtEmail = new MetroFramework.Controls.MetroTextBox
            {
                Location = new Point(30, 144),
                Size = new Size(320, 34),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroTextBoxSize.Medium,
                BackColor = MintBg,
                ForeColor = TextLight,
                WaterMark = "exemple@email.com",
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
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = MintMid,
            };

            // ── TextBox Mot de passe ────────────────────────────────────
            txtPassword = new MetroFramework.Controls.MetroTextBox
            {
                Location = new Point(30, 216),
                Size = new Size(320, 34),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroTextBoxSize.Medium,
                BackColor = MintBg,
                ForeColor = TextLight,
                PasswordChar = '●',
                WaterMark = "••••••••",
                WaterMarkColor = TextMuted,
                UseSelectable = true,
            };

            // ── Bouton Se connecter ─────────────────────────────────────
            btnLogin = new MetroFramework.Controls.MetroButton
            {
                Text = "Se connecter",
                Location = new Point(30, 278),
                Size = new Size(320, 40),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroButtonSize.Medium,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                BackColor = Gold,
                ForeColor = Color.FromArgb(12, 12, 12),
                UseSelectable = true,
            };

            // ── Bouton Créer un compte ──────────────────────────────────
            btnRegister = new MetroFramework.Controls.MetroButton
            {
                Text = "Créer un compte",
                Location = new Point(30, 328),
                Size = new Size(320, 36),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Regular,
                BackColor = MintBg,
                ForeColor = MintLight,
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
            pnlCard.Controls.Add(btnRegister);

            this.Controls.Add(pnlCard);
        }
    }
}