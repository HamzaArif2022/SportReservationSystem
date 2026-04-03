using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.ClientApp.Forms
{
    partial class FormRegister
    {
        private System.ComponentModel.IContainer components = null;

        private static readonly Color MintBg = Color.FromArgb(0, 26, 18);
        private static readonly Color MintMid = Color.FromArgb(0, 42, 26);
        private static readonly Color MintAccent = Color.FromArgb(5, 150, 105);
        private static readonly Color MintLight = Color.FromArgb(110, 231, 183);
        private static readonly Color Gold = Color.FromArgb(251, 191, 36);
        private static readonly Color TextLight = Color.FromArgb(230, 255, 247);
        private static readonly Color TextMuted = Color.FromArgb(110, 188, 160);

        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblSubtitle;
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroLabel lblNom;
        private MetroFramework.Controls.MetroLabel lblPrenom;
        private MetroFramework.Controls.MetroLabel lblEmail;
        private MetroFramework.Controls.MetroLabel lblTelephone;
        private MetroFramework.Controls.MetroLabel lblPassword;
        private MetroFramework.Controls.MetroLabel lblConfirmPassword;
        private MetroFramework.Controls.MetroTextBox txtNom;
        private MetroFramework.Controls.MetroTextBox txtPrenom;
        private MetroFramework.Controls.MetroTextBox txtEmail;
        private MetroFramework.Controls.MetroTextBox txtTelephone;
        private MetroFramework.Controls.MetroTextBox txtPassword;
        private MetroFramework.Controls.MetroTextBox txtConfirmPassword;
        private MetroFramework.Controls.MetroButton btnRegister;
        private MetroFramework.Controls.MetroButton btnCancel;
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

            this.Text = "Inscription — Sport Réservation";
            this.ClientSize = new Size(480, 600);
            this.MinimumSize = new Size(480, 600);
            this.MaximumSize = new Size(480, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Resizable = false;
            this.BackColor = MintBg;

            lblTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Sport Réservation",
                Location = new Point(0, 32),
                Size = new Size(480, 32),
                FontSize = MetroFramework.MetroLabelSize.Tall,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = MintLight,
                BackColor = MintBg,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            lblSubtitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "Créer votre compte client",
                Location = new Point(0, 68),
                Size = new Size(480, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = MintBg,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            lblDivider = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(30, 96),
                Size = new Size(420, 2),
                BackColor = MintAccent,
                ForeColor = MintAccent,
            };

            pnlCard = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(30, 108),
                Size = new Size(420, 460),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            MetroFramework.Controls.MetroLabel MakeLbl(string text, int y) =>
                new MetroFramework.Controls.MetroLabel
                {
                    Text = text,
                    Location = new Point(16, y),
                    Size = new Size(130, 20),
                    FontSize = MetroFramework.MetroLabelSize.Small,
                    FontWeight = MetroFramework.MetroLabelWeight.Bold,
                    Style = MetroFramework.MetroColorStyle.Teal,
                    Theme = MetroFramework.MetroThemeStyle.Dark,
                    ForeColor = TextMuted,
                    BackColor = MintMid,
                };

            MetroFramework.Controls.MetroTextBox MakeTxt(string wm, int y, bool pwd = false) =>
                new MetroFramework.Controls.MetroTextBox
                {
                    Location = new Point(152, y),
                    Size = new Size(252, 30),
                    Style = MetroFramework.MetroColorStyle.Teal,
                    Theme = MetroFramework.MetroThemeStyle.Dark,
                    BackColor = MintBg,
                    ForeColor = TextLight,
                    WaterMark = wm,
                    WaterMarkColor = TextMuted,
                    PasswordChar = pwd ? '●' : '\0',
                    UseSelectable = true,
                };

            lblNom = MakeLbl("Nom", 16);
            txtNom = MakeTxt("Votre nom", 14);
            lblPrenom = MakeLbl("Prénom", 60);
            txtPrenom = MakeTxt("Votre prénom", 58);
            lblEmail = MakeLbl("Email", 104);
            txtEmail = MakeTxt("exemple@mail.com", 102);
            lblTelephone = MakeLbl("Téléphone", 148);
            txtTelephone = MakeTxt("06 XX XX XX XX", 146);
            lblPassword = MakeLbl("Mot de passe", 192);
            txtPassword = MakeTxt("••••••••", 190, pwd: true);
            lblConfirmPassword = MakeLbl("Confirmer", 236);
            txtConfirmPassword = MakeTxt("••••••••", 234, pwd: true);

            btnRegister = new MetroFramework.Controls.MetroButton
            {
                Text = "Créer mon compte",
                Location = new Point(16, 300),
                Size = new Size(388, 40),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = Gold,
                ForeColor = Color.FromArgb(12, 12, 12),
                FontSize = MetroFramework.MetroButtonSize.Medium,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            btnCancel = new MetroFramework.Controls.MetroButton
            {
                Text = "Annuler",
                Location = new Point(16, 350),
                Size = new Size(388, 34),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintBg,
                ForeColor = MintLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                UseSelectable = true,
            };

            pnlCard.Controls.Add(lblNom); pnlCard.Controls.Add(txtNom);
            pnlCard.Controls.Add(lblPrenom); pnlCard.Controls.Add(txtPrenom);
            pnlCard.Controls.Add(lblEmail); pnlCard.Controls.Add(txtEmail);
            pnlCard.Controls.Add(lblTelephone); pnlCard.Controls.Add(txtTelephone);
            pnlCard.Controls.Add(lblPassword); pnlCard.Controls.Add(txtPassword);
            pnlCard.Controls.Add(lblConfirmPassword); pnlCard.Controls.Add(txtConfirmPassword);
            pnlCard.Controls.Add(btnRegister);
            pnlCard.Controls.Add(btnCancel);

            this.Controls.Add(pnlCard);
            this.Controls.Add(lblDivider);
            this.Controls.Add(lblSubtitle);
            this.Controls.Add(lblTitle);
        }
    }
}