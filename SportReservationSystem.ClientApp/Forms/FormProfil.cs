using System;
using System.Windows.Forms;
using SportReservationSystem.ClientApp;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormProfil : Form
    {
        private readonly ApiClient _apiClient;
        private readonly int _clientId;
        private UserDto _user;

        // Contrôles
        private TextBox txtNom;
        private TextBox txtPrenom;
        private TextBox txtEmail;
        private TextBox txtTelephone;
        private Label lblRole;
        private Button btnModifier;
        private GroupBox groupBoxProfil;
        private GroupBox groupBoxMdp;
        private TextBox txtAncienMdp;
        private TextBox txtNouveauMdp;
        private TextBox txtConfirmerMdp;
        private Button btnChangerMdp;
        private Label lblNom;
        private Label lblPrenom;
        private Label lblEmail;
        private Label lblTelephone;
        private Label lblRoleTitle;
        private Label lblAncienMdp;
        private Label lblNouveauMdp;
        private Label lblConfirmerMdp;

        public FormProfil(ApiClient apiClient, int clientId)
        {
            _apiClient = apiClient;
            _clientId = clientId;
            InitializeComponent();
            Load += async (_, _) => await LoadProfilAsync();
            
            btnModifier.Click += async (_, _) => await ModifierProfilAsync();
            btnChangerMdp.Click += async (_, _) => await ChangerMotDePasseAsync();
        }

        private void InitializeComponent()
        {
            this.groupBoxProfil = new GroupBox();
            this.lblRoleTitle = new Label();
            this.lblRole = new Label();
            this.txtTelephone = new TextBox();
            this.lblTelephone = new Label();
            this.txtEmail = new TextBox();
            this.lblEmail = new Label();
            this.txtPrenom = new TextBox();
            this.lblPrenom = new Label();
            this.txtNom = new TextBox();
            this.lblNom = new Label();
            this.btnModifier = new Button();
            
            this.groupBoxMdp = new GroupBox();
            this.txtConfirmerMdp = new TextBox();
            this.lblConfirmerMdp = new Label();
            this.txtNouveauMdp = new TextBox();
            this.lblNouveauMdp = new Label();
            this.txtAncienMdp = new TextBox();
            this.lblAncienMdp = new Label();
            this.btnChangerMdp = new Button();
            
            this.SuspendLayout();
            
            // ============================================
            // GroupBox Profil
            // ============================================
            this.groupBoxProfil.Location = new System.Drawing.Point(20, 20);
            this.groupBoxProfil.Size = new System.Drawing.Size(400, 280);
            this.groupBoxProfil.Text = "Informations personnelles";
            
            // lblNom
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(20, 35);
            this.lblNom.Text = "Nom:";
            
            // txtNom
            this.txtNom.Location = new System.Drawing.Point(120, 32);
            this.txtNom.Size = new System.Drawing.Size(250, 23);
            
            // lblPrenom
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Location = new System.Drawing.Point(20, 75);
            this.lblPrenom.Text = "Prénom:";
            
            // txtPrenom
            this.txtPrenom.Location = new System.Drawing.Point(120, 72);
            this.txtPrenom.Size = new System.Drawing.Size(250, 23);
            
            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 115);
            this.lblEmail.Text = "Email:";
            
            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(120, 112);
            this.txtEmail.Size = new System.Drawing.Size(250, 23);
            
            // lblTelephone
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(20, 155);
            this.lblTelephone.Text = "Téléphone:";
            
            // txtTelephone
            this.txtTelephone.Location = new System.Drawing.Point(120, 152);
            this.txtTelephone.Size = new System.Drawing.Size(250, 23);
            
            // lblRoleTitle
            this.lblRoleTitle.AutoSize = true;
            this.lblRoleTitle.Location = new System.Drawing.Point(20, 195);
            this.lblRoleTitle.Text = "Rôle:";
            
            // lblRole
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRole.Location = new System.Drawing.Point(120, 195);
            this.lblRole.Text = "";
            
            // btnModifier
            this.btnModifier.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnModifier.FlatStyle = FlatStyle.Flat;
            this.btnModifier.ForeColor = System.Drawing.Color.White;
            this.btnModifier.Location = new System.Drawing.Point(120, 235);
            this.btnModifier.Size = new System.Drawing.Size(100, 30);
            this.btnModifier.Text = "Modifier";
            this.btnModifier.UseVisualStyleBackColor = false;
            
            this.groupBoxProfil.Controls.Add(this.lblNom);
            this.groupBoxProfil.Controls.Add(this.txtNom);
            this.groupBoxProfil.Controls.Add(this.lblPrenom);
            this.groupBoxProfil.Controls.Add(this.txtPrenom);
            this.groupBoxProfil.Controls.Add(this.lblEmail);
            this.groupBoxProfil.Controls.Add(this.txtEmail);
            this.groupBoxProfil.Controls.Add(this.lblTelephone);
            this.groupBoxProfil.Controls.Add(this.txtTelephone);
            this.groupBoxProfil.Controls.Add(this.lblRoleTitle);
            this.groupBoxProfil.Controls.Add(this.lblRole);
            this.groupBoxProfil.Controls.Add(this.btnModifier);
            
            // ============================================
            // GroupBox Mot de passe
            // ============================================
            this.groupBoxMdp.Location = new System.Drawing.Point(450, 20);
            this.groupBoxMdp.Size = new System.Drawing.Size(400, 280);
            this.groupBoxMdp.Text = "Changer le mot de passe";
            
            // lblAncienMdp
            this.lblAncienMdp.AutoSize = true;
            this.lblAncienMdp.Location = new System.Drawing.Point(20, 35);
            this.lblAncienMdp.Text = "Ancien mot de passe:";
            
            // txtAncienMdp
            this.txtAncienMdp.Location = new System.Drawing.Point(180, 32);
            this.txtAncienMdp.Size = new System.Drawing.Size(200, 23);
            this.txtAncienMdp.PasswordChar = '*';
            
            // lblNouveauMdp
            this.lblNouveauMdp.AutoSize = true;
            this.lblNouveauMdp.Location = new System.Drawing.Point(20, 75);
            this.lblNouveauMdp.Text = "Nouveau mot de passe:";
            
            // txtNouveauMdp
            this.txtNouveauMdp.Location = new System.Drawing.Point(180, 72);
            this.txtNouveauMdp.Size = new System.Drawing.Size(200, 23);
            this.txtNouveauMdp.PasswordChar = '*';
            
            // lblConfirmerMdp
            this.lblConfirmerMdp.AutoSize = true;
            this.lblConfirmerMdp.Location = new System.Drawing.Point(20, 115);
            this.lblConfirmerMdp.Text = "Confirmer le mot de passe:";
            
            // txtConfirmerMdp
            this.txtConfirmerMdp.Location = new System.Drawing.Point(180, 112);
            this.txtConfirmerMdp.Size = new System.Drawing.Size(200, 23);
            this.txtConfirmerMdp.PasswordChar = '*';
            
            // btnChangerMdp
            this.btnChangerMdp.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnChangerMdp.FlatStyle = FlatStyle.Flat;
            this.btnChangerMdp.ForeColor = System.Drawing.Color.White;
            this.btnChangerMdp.Location = new System.Drawing.Point(180, 160);
            this.btnChangerMdp.Size = new System.Drawing.Size(120, 30);
            this.btnChangerMdp.Text = "Changer";
            this.btnChangerMdp.UseVisualStyleBackColor = false;
            
            this.groupBoxMdp.Controls.Add(this.lblAncienMdp);
            this.groupBoxMdp.Controls.Add(this.txtAncienMdp);
            this.groupBoxMdp.Controls.Add(this.lblNouveauMdp);
            this.groupBoxMdp.Controls.Add(this.txtNouveauMdp);
            this.groupBoxMdp.Controls.Add(this.lblConfirmerMdp);
            this.groupBoxMdp.Controls.Add(this.txtConfirmerMdp);
            this.groupBoxMdp.Controls.Add(this.btnChangerMdp);
            
            // ============================================
            // Form
            // ============================================
            this.ClientSize = new System.Drawing.Size(880, 330);
            this.Controls.Add(this.groupBoxProfil);
            this.Controls.Add(this.groupBoxMdp);
            this.Text = "Mon Profil";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.White;
            
            this.ResumeLayout(false);
        }

        private async Task LoadProfilAsync()
        {
            var response = await _apiClient.GetAsync<AuthResponseDto>($"api/auth/user/{_clientId}");
            if (response?.User != null)
            {
                _user = response.User;
                txtNom.Text = _user.Nom;
                txtPrenom.Text = _user.Prenom;
                txtEmail.Text = _user.Email;
                txtTelephone.Text = _user.Telephone;
                lblRole.Text = _user.Role;
            }
        }

        private async Task ModifierProfilAsync()
        {
            var dto = new
            {
                Nom = txtNom.Text,
                Prenom = txtPrenom.Text,
                Email = txtEmail.Text,
                Telephone = txtTelephone.Text
            };
            
            var success = await _apiClient.PutAsync("api/auth/profil", dto);
            
            if (success)
            {
                MessageBox.Show("Profil modifié avec succès.", "Succès", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification.", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ChangerMotDePasseAsync()
        {
            if (txtNouveauMdp.Text != txtConfirmerMdp.Text)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var dto = new
            {
                AncienMotDePasse = txtAncienMdp.Text,
                NouveauMotDePasse = txtNouveauMdp.Text
            };
            
            var success = await _apiClient.PutAsync("api/auth/changer-mot-de-passe", dto);
            
            if (success)
            {
                MessageBox.Show("Mot de passe modifié avec succès.", "Succès", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAncienMdp.Text = "";
                txtNouveauMdp.Text = "";
                txtConfirmerMdp.Text = "";
            }
            else
            {
                MessageBox.Show("Erreur lors du changement de mot de passe.", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}