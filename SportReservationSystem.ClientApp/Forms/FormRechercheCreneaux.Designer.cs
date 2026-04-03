using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.ClientApp.Forms
{
    partial class FormRechercheCreneaux
    {
        private System.ComponentModel.IContainer components = null;

        private static readonly Color MintBg = Color.FromArgb(0, 26, 18);
        private static readonly Color MintMid = Color.FromArgb(0, 42, 26);
        private static readonly Color MintAccent = Color.FromArgb(5, 150, 105);
        private static readonly Color MintLight = Color.FromArgb(110, 231, 183);
        private static readonly Color Gold = Color.FromArgb(251, 191, 36);
        private static readonly Color TextLight = Color.FromArgb(230, 255, 247);
        private static readonly Color TextMuted = Color.FromArgb(110, 188, 160);
        private static readonly Color CardBg = Color.FromArgb(0, 34, 22);

        private MetroFramework.Controls.MetroLabel lblTitle;
        private MetroFramework.Controls.MetroLabel lblDivider;
        private MetroFramework.Controls.MetroLabel lblTerrain;
        private MetroFramework.Controls.MetroLabel lblDate;
        private MetroFramework.Controls.MetroLabel lblStatus;
        private MetroFramework.Controls.MetroPanel pnlTop;
        private MetroFramework.Controls.MetroComboBox cmbTerrain;
        private MetroFramework.Controls.MetroButton btnRechercher;
        private MetroFramework.Controls.MetroGrid dataGridViewCreneaux;
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

            this.Text = "Rechercher un Créneau — Sport Réservation";
            this.ClientSize = new Size(900, 580);
            this.MinimumSize = new Size(800, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.BackColor = MintBg;

            lblTitle = new MetroFramework.Controls.MetroLabel
            {
                Text = "🔍  Rechercher un Créneau",
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

            lblDivider = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(0, 68),
                Size = new Size(900, 2),
                BackColor = MintAccent,
                ForeColor = MintAccent,
            };

            pnlTop = new MetroFramework.Controls.MetroPanel
            {
                Location = new Point(0, 72),
                Size = new Size(900, 72),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintMid,
            };

            lblTerrain = new MetroFramework.Controls.MetroLabel
            {
                Text = "Terrain :",
                Location = new Point(20, 26),
                Size = new Size(76, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = MintMid,
            };

            cmbTerrain = new MetroFramework.Controls.MetroComboBox
            {
                Location = new Point(102, 22),
                Size = new Size(240, 32),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = MintBg,
                ForeColor = TextLight,
                FormattingEnabled = true,
                UseSelectable = true,
            };

            lblDate = new MetroFramework.Controls.MetroLabel
            {
                Text = "Date :",
                Location = new Point(358, 26),
                Size = new Size(54, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                FontWeight = MetroFramework.MetroLabelWeight.Bold,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = MintMid,
            };

            dtpDate = new DateTimePicker
            {
                Location = new Point(418, 24),
                Size = new Size(200, 28),
                Format = DateTimePickerFormat.Short,
                BackColor = MintBg,
                ForeColor = TextLight,
                CalendarTitleBackColor = MintAccent,
                CalendarTitleForeColor = TextLight,
                CalendarMonthBackground = MintMid,
            };

            btnRechercher = new MetroFramework.Controls.MetroButton
            {
                Text = "🔍  Rechercher",
                Location = new Point(634, 20),
                Size = new Size(150, 34),
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                BackColor = Gold,
                ForeColor = Color.FromArgb(12, 12, 12),
                FontSize = MetroFramework.MetroButtonSize.Small,
                FontWeight = MetroFramework.MetroButtonWeight.Bold,
                UseSelectable = true,
            };

            pnlTop.Controls.Add(lblTerrain);
            pnlTop.Controls.Add(cmbTerrain);
            pnlTop.Controls.Add(lblDate);
            pnlTop.Controls.Add(dtpDate);
            pnlTop.Controls.Add(btnRechercher);

            dataGridViewCreneaux = new MetroFramework.Controls.MetroGrid
            {
                Location = new Point(20, 158),
                Size = new Size(860, 380),
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

            lblStatus = new MetroFramework.Controls.MetroLabel
            {
                Text = "",
                Location = new Point(20, 546),
                Size = new Size(500, 22),
                FontSize = MetroFramework.MetroLabelSize.Small,
                Style = MetroFramework.MetroColorStyle.Teal,
                Theme = MetroFramework.MetroThemeStyle.Dark,
                ForeColor = TextMuted,
                BackColor = MintBg,
            };

            this.Controls.Add(dataGridViewCreneaux);
            this.Controls.Add(lblStatus);
            this.Controls.Add(pnlTop);
            this.Controls.Add(lblDivider);
            this.Controls.Add(lblTitle);
        }
    }
}