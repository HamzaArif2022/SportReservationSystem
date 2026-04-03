using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.ClientApp.Forms
{
    partial class FormProfil
    {
        private System.ComponentModel.IContainer components = null;

        private static readonly Color MintBg = Color.FromArgb(0, 26, 18);
        private static readonly Color MintMid = Color.FromArgb(0, 42, 26);
        private static readonly Color MintAccent = Color.FromArgb(5, 150, 105);
        private static readonly Color MintLight = Color.FromArgb(110, 231, 183);
        private static readonly Color Gold = Color.FromArgb(251, 191, 36);
        private static readonly Color TextLight = Color.FromArgb(230, 255, 247);
        private static readonly Color TextMuted = Color.FromArgb(110, 188, 160);
        private static readonly Color DangerRed = Color.FromArgb(220, 50, 50);

        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroPanel pnlProfil;
        private MetroFramework.Controls.MetroPanel pnlMdp;
        private MetroFramework.Controls.MetroLabel lblProfilTitle;
        private MetroFramework.Controls.MetroLabel lblMdpTitle;
        private MetroFramework.Controls.MetroLabel lblNom;
        private MetroFramework.Controls.MetroLabel lblPrenom;
        private MetroFramework.Controls.MetroLabel lblEmail;
        private MetroFramework.Controls.MetroLabel lblTelephone;
        private MetroFramework.Controls.MetroLabel lblRoleTitle;
        private MetroFramework.Controls.MetroLabel lblRole;
        private MetroFramework.Controls.MetroTextBox txtNom;
        private MetroFramework.Controls.MetroTextBox txtPrenom;
        private MetroFramework.Controls.MetroTextBox txtEmail;
        private MetroFramework.Controls.MetroTextBox txtTelephone;
        private MetroFramework.Controls.MetroButton btnModifier;
        private MetroFramework.Controls.MetroLabel lblAncienMdp;
        private MetroFramework.Controls.MetroLabel lblNouveauMdp;
        private MetroFramework.Controls.MetroLabel lblConfirmerMdp;
        private MetroFramework.Controls.MetroTextBox txtAncienMdp;
        private MetroFramework.Controls.MetroTextBox txtNouveauMdp;
        private MetroFramework.Controls.MetroTextBox txtConfirmerMdp;
        private MetroFramework.Controls.MetroButton btnChangerMdp;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.Text = "Mon Profil — Sport Réservation";
            this.ClientSize = new Size(980, 480);
            this.MinimumSize = new Size(980, 480);
            this.MaximumSize = new Size(980, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Resizable = false;
            this.BackColor = MintBg;

            lblTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "👤  Mon Profil",
                Location = new Point(0, 32),
                Size = new Size(980, 32),
                FontSize = MetroFramework.MetroLabelSize.Medium,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = MintLight,
                BackColor = MintBg,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            lblDivider = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(0, 68),
                Size = new Size(980, 2),
                BackColor = MintAccent,
                ForeColor = MintAccent,
            };

            // helper champ
            MetroFramework.Controls.MetroLabel MakeLbl(string text, int y, MetroFramework.Controls.MetroPanel parent) =>
                new MetroFramework.Controls.MetroLabel
                {
                    Text = text,
                    Location = new Point(16, y),
                    Size = new Size(140, 22),
                    FontSize = MetroFramework.MetroLabelSize.Small,
                    FontWeight = MetroFramework.MetroLabelWeight.Bold,
                    Style = MetroFramework.MetroColorStyle.Teal,
                    Theme = MetroFramework.MetroThemeStyle.Dark,
                    ForeColor = TextMuted,
                    BackColor = MintMid,
                };

            MetroFramework.Controls.MetroTextBox MakeTxt(int y, bool pwd = false) =>
                new MetroFramework.Controls.MetroTextBox
                {
                    Location = new Point(162, y),
                    Size = new Size(260, 30),
                    Style = MetroFramework.MetroColorStyle.Teal,
                    Theme = MetroFramework.MetroThemeStyle.Dark,
                    BackColor = MintBg,
                    ForeColor = TextLight,
                    PasswordChar = pwd ? '●' : '\0',
                    UseSelectable = true,
                };

            // ── Panel Profil ─────────────────────────────────────────────
            pnlProfil = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(20, 82),
                Size = new Size(454, 368),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            lblProfilTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Informations personnelles",
                Location = new Point(16, 14),
                Size = new Size(420, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = MintLight,
                BackColor = MintMid,
            };

            lblNom = MakeLbl("Nom", 50, pnlProfil);
            txtNom = MakeTxt(48);
            lblPrenom = MakeLbl("Prénom", 96, pnlProfil);
            txtPrenom = MakeTxt(94);
            lblEmail = MakeLbl("Email", 142, pnlProfil);
            txtEmail = MakeTxt(140);
            lblTelephone = MakeLbl("Téléphone", 188, pnlProfil);
            txtTelephone = MakeTxt(186);

            lblRoleTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Rôle",
                Location = new Point(16, 234),
                Size = new Size(140, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = MintMid,
            };

            lblRole = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(162, 234),
                Size = new Size(260, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = Gold,
                BackColor = MintMid,
            };

            btnModifier = new MetroFramework.Controls.MetroButton
            {
                Text = "💾  Enregistrer",
                Location = new Point(162, 300),
                Size = new Size(160, 36),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = Gold,
                ForeColor = Color.FromArgb(12, 12, 12),
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            pnlProfil.Controls.Add(lblProfilTitle);
            pnlProfil.Controls.Add(lblNom); pnlProfil.Controls.Add(txtNom);
            pnlProfil.Controls.Add(lblPrenom); pnlProfil.Controls.Add(txtPrenom);
            pnlProfil.Controls.Add(lblEmail); pnlProfil.Controls.Add(txtEmail);
            pnlProfil.Controls.Add(lblTelephone); pnlProfil.Controls.Add(txtTelephone);
            pnlProfil.Controls.Add(lblRoleTitle); pnlProfil.Controls.Add(lblRole);
            pnlProfil.Controls.Add(btnModifier);

            // ── Panel Mot de passe ───────────────────────────────────────
            pnlMdp = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(494, 82),
                Size = new Size(466, 368),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            lblMdpTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Changer le mot de passe",
                Location = new Point(16, 14),
                Size = new Size(430, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = MintLight,
                BackColor = MintMid,
            };

            lblAncienMdp = MakeLbl("Ancien mot de passe", 50, pnlMdp);
            txtAncienMdp = MakeTxt(48, pwd: true);
            lblNouveauMdp = MakeLbl("Nouveau mot de passe", 96, pnlMdp);
            txtNouveauMdp = MakeTxt(94, pwd: true);
            lblConfirmerMdp = MakeLbl("Confirmer", 142, pnlMdp);
            txtConfirmerMdp = MakeTxt(140, pwd: true);

            btnChangerMdp = new MetroFramework.Controls.MetroButton
            {
                Text = "🔑  Changer le mot de passe",
                Location = new Point(16, 210),
                Size = new Size(220, 36),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintAccent,
                ForeColor = TextLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            pnlMdp.Controls.Add(lblMdpTitle);
            pnlMdp.Controls.Add(lblAncienMdp); pnlMdp.Controls.Add(txtAncienMdp);
            pnlMdp.Controls.Add(lblNouveauMdp); pnlMdp.Controls.Add(txtNouveauMdp);
            pnlMdp.Controls.Add(lblConfirmerMdp); pnlMdp.Controls.Add(txtConfirmerMdp);
            pnlMdp.Controls.Add(btnChangerMdp);

            this.Controls.Add(pnlProfil);
            this.Controls.Add(pnlMdp);
            this.Controls.Add(lblDivider);
            this.Controls.Add(lblTitle);
        }
    }
}