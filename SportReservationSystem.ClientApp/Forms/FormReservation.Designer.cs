using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.ClientApp.Forms
{
    partial class FormReservation
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
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroLabel lblTerrain;
        private MetroFramework.Controls.MetroLabel lblDate;
        private MetroFramework.Controls.MetroLabel lblHeure;
        private MetroFramework.Controls.MetroLabel lblTarif;
        private MetroFramework.Controls.MetroLabel lblTotal;
        private MetroFramework.Controls.MetroTextBox txtTerrain;
        private MetroFramework.Controls.MetroTextBox txtDate;
        private MetroFramework.Controls.MetroTextBox txtHeure;
        private MetroFramework.Controls.MetroTextBox txtTarif;
        private MetroFramework.Controls.MetroTextBox txtTotal;
        private MetroFramework.Controls.MetroButton btnConfirmer;
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

            this.Text = "Confirmer la Réservation — Sport Réservation";
            this.ClientSize = new Size(520, 460);
            this.MinimumSize = new Size(520, 460);
            this.MaximumSize = new Size(520, 460);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Resizable = false;
            this.BackColor = MintBg;

            lblTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "🎾  Confirmer la Réservation",
                Location = new Point(0, 32),
                Size = new Size(520, 32),
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
                Size = new Size(520, 2),
                BackColor = MintAccent,
                ForeColor = MintAccent,
            };

            pnlCard = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(30, 82),
                Size = new Size(460, 316),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            MetroFramework.Controls.MetroLabel MakeLbl(string text, int y) =>
                new MetroFramework.Controls.MetroLabel
                {
                    Text = text,
                    Location = new Point(16, y),
                    Size = new Size(120, 22),
                    FontSize = MetroFramework.MetroLabelSize.Small,
                    FontWeight = MetroFramework.MetroLabelWeight.Bold,
                    Style = MetroFramework.MetroColorStyle.Teal,
                    Theme = MetroFramework.MetroThemeStyle.Dark,
                    ForeColor = TextMuted,
                    BackColor = MintMid,
                };

            MetroFramework.Controls.MetroTextBox MakeTxt(int y, bool big = false) =>
                new MetroFramework.Controls.MetroTextBox
                {
                    Location = new Point(146, y),
                    Size = new Size(298, big ? 36 : 30),
                    Style = MetroFramework.MetroColorStyle.Teal,
                    Theme = MetroFramework.MetroThemeStyle.Dark,
                    BackColor = MintBg,
                    ForeColor = big ? Gold : TextLight,
                    ReadOnly = true,
                    UseSelectable = true,
                    FontSize = big ? MetroFramework.MetroTextBoxSize.Medium : MetroFramework.MetroTextBoxSize.Small,
                };

            lblTerrain = MakeLbl("Terrain", 16); txtTerrain = MakeTxt(14);
            lblDate = MakeLbl("Date", 62); txtDate = MakeTxt(60);
            lblHeure = MakeLbl("Horaire", 108); txtHeure = MakeTxt(106);
            lblTarif = MakeLbl("Tarif/h", 154); txtTarif = MakeTxt(152);
            lblTotal = MakeLbl("💰 Total", 208); txtTotal = MakeTxt(204, big: true);

            pnlCard.Controls.Add(lblTerrain); pnlCard.Controls.Add(txtTerrain);
            pnlCard.Controls.Add(lblDate); pnlCard.Controls.Add(txtDate);
            pnlCard.Controls.Add(lblHeure); pnlCard.Controls.Add(txtHeure);
            pnlCard.Controls.Add(lblTarif); pnlCard.Controls.Add(txtTarif);
            pnlCard.Controls.Add(lblTotal); pnlCard.Controls.Add(txtTotal);

            pnlBottom = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(0, 406),
                Size = new Size(520, 54),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            btnConfirmer = new MetroFramework.Controls.MetroButton
            {
                Text = "✔  Confirmer et payer",
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

            pnlBottom.Controls.Add(btnConfirmer);
            pnlBottom.Controls.Add(btnAnnuler);

            this.Controls.Add(pnlCard);
            this.Controls.Add(lblDivider);
            this.Controls.Add(pnlBottom);
            this.Controls.Add(lblTitle);
        }
    }
}