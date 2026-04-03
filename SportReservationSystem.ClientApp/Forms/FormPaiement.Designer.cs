using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.ClientApp.Forms
{
    partial class FormPaiement
    {
        private System.ComponentModel.IContainer components = null;

        private static readonly Color MintBg = Color.FromArgb(0, 26, 18);
        private static readonly Color MintMid = Color.FromArgb(0, 42, 26);
        private static readonly Color MintAccent = Color.FromArgb(5, 150, 105);
        private static readonly Color MintLight = Color.FromArgb(110, 231, 183);
        private static readonly Color Gold = Color.FromArgb(251, 191, 36);
        private static readonly Color GoldDim = Color.FromArgb(140, 105, 15);
        private static readonly Color TextLight = Color.FromArgb(230, 255, 247);
        private static readonly Color TextMuted = Color.FromArgb(110, 188, 160);

        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroLabel lblReservation;
        private MetroFramework.Controls.MetroLabel lblMontant;
        private MetroFramework.Controls.MetroLabel lblModePaiement;
        private MetroFramework.Controls.MetroLabel lblMontantVal;
        private MetroFramework.Controls.MetroTextBox txtReservationId;
        private MetroFramework.Controls.MetroTextBox txtMontant;
        private MetroFramework.Controls.MetroComboBox cmbModePaiement;
        private MetroFramework.Controls.MetroButton btnPayer;
        private MetroFramework.Controls.MetroButton btnAnnuler;
        private MetroFramework.Controls.MetroPanel pnlCard;
        private MetroFramework.Controls.MetroPanel pnlBottom;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.Text = "Paiement — Sport Réservation";
            this.ClientSize = new Size(480, 420);
            this.MinimumSize = new Size(480, 420);
            this.MaximumSize = new Size(480, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Resizable = false;
            this.BackColor = MintBg;

            lblTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "💳  Paiement de la Réservation",
                Location = new Point(0, 32),
                Size = new Size(480, 32),
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
                Size = new Size(480, 2),
                BackColor = MintAccent,
                ForeColor = MintAccent,
            };

            pnlCard = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(30, 82),
                Size = new Size(420, 270),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            MetroFramework.Controls.MetroLabel MakeLbl(string text, int y) =>
                new MetroFramework.Controls.MetroLabel
                {
                    Text = text,
                    Location = new Point(20, y),
                    Size = new Size(140, 22),
                    FontSize = MetroFramework.MetroLabelSize.Small,
                    FontWeight = MetroFramework.MetroLabelWeight.Bold,
                    Style = MetroFramework.MetroColorStyle.Teal,
                    Theme = MetroFramework.MetroThemeStyle.Dark,
                    ForeColor = TextMuted,
                    BackColor = MintMid,
                };

            lblReservation = MakeLbl("N° Réservation", 20);
            lblMontant = MakeLbl("Montant", 70);
            lblModePaiement = MakeLbl("Mode de paiement", 120);

            txtReservationId = new MetroFramework.Controls.MetroTextBox
            {
                Location = new Point(170, 18),
                Size = new Size(230, 30),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintBg,
                ForeColor = TextLight,
                ReadOnly = true,
                UseSelectable = true,
            };

            txtMontant = new MetroFramework.Controls.MetroTextBox
            {
                Location = new Point(170, 68),
                Size = new Size(230, 30),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintBg,
                ForeColor = Gold,
                ReadOnly = true,
                UseSelectable = true,
                FontSize = MetroFramework.MetroTextBoxSize.Medium,
            };

            cmbModePaiement = new MetroFramework.Controls.MetroComboBox
            {
                Location = new Point(170, 118),
                Size = new Size(230, 32),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintBg,
                ForeColor = TextLight,
                FormattingEnabled = true,
                UseSelectable = true,
            };
            cmbModePaiement.Items.AddRange(new object[] { "Carte", "Espèce", "Virement" });

            // Montant récapitulatif
            lblMontantVal = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(20, 185),
                Size = new Size(380, 52),
                FontSize = MetroFramework.MetroLabelSize.Tall,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = Gold,
                BackColor = MintMid,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            pnlCard.Controls.Add(lblReservation);
            pnlCard.Controls.Add(txtReservationId);
            pnlCard.Controls.Add(lblMontant);
            pnlCard.Controls.Add(txtMontant);
            pnlCard.Controls.Add(lblModePaiement);
            pnlCard.Controls.Add(cmbModePaiement);
            pnlCard.Controls.Add(lblMontantVal);

            pnlBottom = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(0, 362),
                Size = new Size(480, 54),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            btnPayer = new MetroFramework.Controls.MetroButton
            {
                Text = "💳  Payer maintenant",
                Location = new Point(20, 12),
                Size = new Size(200, 32),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = Gold,
                ForeColor = Color.FromArgb(12, 12, 12),
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            btnAnnuler = new MetroFramework.Controls.MetroButton
            {
                Text = "Annuler",
                Location = new Point(232, 12),
                Size = new Size(120, 32),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintBg,
                ForeColor = MintLight,
                FontSize = MetroFramework.MetroButtonSize.Small,
                UseSelectable = true,
            };

            pnlBottom.Controls.Add(btnPayer);
            pnlBottom.Controls.Add(btnAnnuler);

            this.Controls.Add(pnlCard);
            this.Controls.Add(lblDivider);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(lblTitle);
        }
    }
}