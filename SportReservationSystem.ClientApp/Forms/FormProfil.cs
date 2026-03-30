using System.Windows.Forms;
using MetroFramework.Forms;
using SportReservationSystem.ClientApp;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormProfil : MetroForm
    {
        private readonly ApiClient _apiClient;
        private readonly int _clientId;
        private UserDto _user;

        public FormProfil(ApiClient apiClient, int clientId)
        {
            _apiClient = apiClient;
            _clientId = clientId;
            InitializeComponent();
            Load += async (_, _) => await LoadProfilAsync();
            
            btnModifier.Click += async (_, _) => await ModifierProfilAsync();
            btnChangerMdp.Click += async (_, _) => await ChangerMotDePasseAsync();
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
            
            var success = await _apiClient.PutAsync("api/auth/profil");
            
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
            
            var success = await _apiClient.PutAsync("api/auth/changer-mot-de-passe");
            
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

        private void InitializeComponent()
        {
            this.lblNom = new MetroFramework.Controls.MetroLabel();
            this.txtNom = new MetroFramework.Controls.MetroTextBox();
            this.lblPrenom = new MetroFramework.Controls.MetroLabel();
            this.txtPrenom = new MetroFramework.Controls.MetroTextBox();
            this.lblEmail = new MetroFramework.Controls.MetroLabel();
            this.txtEmail = new MetroFramework.Controls.MetroTextBox();
            this.lblTelephone = new MetroFramework.Controls.MetroLabel();
            this.txtTelephone = new MetroFramework.Controls.MetroTextBox();
            this.lblRole = new MetroFramework.Controls.MetroLabel();
            this.btnModifier = new MetroFramework.Controls.MetroButton();
            this.groupBoxProfil = new System.Windows.Forms.GroupBox();
            this.groupBoxMdp = new System.Windows.Forms.GroupBox();
            this.lblConfirmerMdp = new MetroFramework.Controls.MetroLabel();
            this.txtConfirmerMdp = new MetroFramework.Controls.MetroTextBox();
            this.lblNouveauMdp = new MetroFramework.Controls.MetroLabel();
            this.txtNouveauMdp = new MetroFramework.Controls.MetroTextBox();
            this.lblAncienMdp = new MetroFramework.Controls.MetroLabel();
            this.txtAncienMdp = new MetroFramework.Controls.MetroTextBox();
            this.btnChangerMdp = new MetroFramework.Controls.MetroButton();
            this.groupBoxProfil.SuspendLayout();
            this.groupBoxMdp.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(20, 30);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(38, 20);
            this.lblNom.TabIndex = 0;
            this.lblNom.Text = "Nom";
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(120, 30);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(250, 23);
            this.txtNom.TabIndex = 1;
            // 
            // lblPrenom
            // 
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Location = new System.Drawing.Point(20, 70);
            this.lblPrenom.Name = "lblPrenom";
            this.lblPrenom.Size = new System.Drawing.Size(60, 20);
            this.lblPrenom.TabIndex = 2;
            this.lblPrenom.Text = "Prénom";
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(120, 70);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(250, 23);
            this.txtPrenom.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 110);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(43, 20);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(120, 110);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 23);
            this.txtEmail.TabIndex = 5;
            // 
            // lblTelephone
            // 
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Location = new System.Drawing.Point(20, 150);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(73, 20);
            this.lblTelephone.TabIndex = 6;
            this.lblTelephone.Text = "Téléphone";
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(120, 150);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(250, 23);
            this.txtTelephone.TabIndex = 7;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(120, 190);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(36, 20);
            this.lblRole.TabIndex = 8;
            this.lblRole.Text = "Role";
            // 
            // btnModifier
            // 
            this.btnModifier.Location = new System.Drawing.Point(120, 230);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(100, 35);
            this.btnModifier.TabIndex = 9;
            this.btnModifier.Text = "Modifier";
            // 
            // groupBoxProfil
            // 
            this.groupBoxProfil.Controls.Add(this.lblNom);
            this.groupBoxProfil.Controls.Add(this.btnModifier);
            this.groupBoxProfil.Controls.Add(this.txtNom);
            this.groupBoxProfil.Controls.Add(this.lblRole);
            this.groupBoxProfil.Controls.Add(this.lblPrenom);
            this.groupBoxProfil.Controls.Add(this.txtTelephone);
            this.groupBoxProfil.Controls.Add(this.txtPrenom);
            this.groupBoxProfil.Controls.Add(this.lblTelephone);
            this.groupBoxProfil.Controls.Add(this.lblEmail);
            this.groupBoxProfil.Controls.Add(this.txtEmail);
            this.groupBoxProfil.Location = new System.Drawing.Point(23, 30);
            this.groupBoxProfil.Name = "groupBoxProfil";
            this.groupBoxProfil.Size = new System.Drawing.Size(400, 280);
            this.groupBoxProfil.TabIndex = 10;
            this.groupBoxProfil.TabStop = false;
            this.groupBoxProfil.Text = "Informations personnelles";
            // 
            // groupBoxMdp
            // 
            this.groupBoxMdp.Controls.Add(this.lblConfirmerMdp);
            this.groupBoxMdp.Controls.Add(this.txtConfirmerMdp);
            this.groupBoxMdp.Controls.Add(this.lblNouveauMdp);
            this.groupBoxMdp.Controls.Add(this.txtNouveauMdp);
            this.groupBoxMdp.Controls.Add(this.lblAncienMdp);
            this.groupBoxMdp.Controls.Add(this.txtAncienMdp);
            this.groupBoxMdp.Controls.Add(this.btnChangerMdp);
            this.groupBoxMdp.Location = new System.Drawing.Point(450, 30);
            this.groupBoxMdp.Name = "groupBoxMdp";
            this.groupBoxMdp.Size = new System.Drawing.Size(400, 280);
            this.groupBoxMdp.TabIndex = 11;
            this.groupBoxMdp.TabStop = false;
            this.groupBoxMdp.Text = "Changer le mot de passe";
            // 
            // lblConfirmerMdp
            // 
            this.lblConfirmerMdp.AutoSize = true;
            this.lblConfirmerMdp.Location = new System.Drawing.Point(20, 150);
            this.lblConfirmerMdp.Name = "lblConfirmerMdp";
            this.lblConfirmerMdp.Size = new System.Drawing.Size(122, 20);
            this.lblConfirmerMdp.TabIndex = 5;
            this.lblConfirmerMdp.Text = "Confirmer le mot de passe";
            // 
            // txtConfirmerMdp
            // 
            this.txtConfirmerMdp.Location = new System.Drawing.Point(160, 150);
            this.txtConfirmerMdp.Name = "txtConfirmerMdp";
            this.txtConfirmerMdp.PasswordChar = '*';
            this.txtConfirmerMdp.Size = new System.Drawing.Size(200, 23);
            this.txtConfirmerMdp.TabIndex = 4;
            // 
            // lblNouveauMdp
            // 
            this.lblNouveauMdp.AutoSize = true;
            this.lblNouveauMdp.Location = new System.Drawing.Point(20, 110);
            this.lblNouveauMdp.Name = "lblNouveauMdp";
            this.lblNouveauMdp.Size = new System.Drawing.Size(118, 20);
            this.lblNouveauMdp.TabIndex = 3;
            this.lblNouveauMdp.Text = "Nouveau mot de passe";
            // 
            // txtNouveauMdp
            // 
            this.txtNouveauMdp.Location = new System.Drawing.Point(160, 110);
            this.txtNouveauMdp.Name = "txtNouveauMdp";
            this.txtNouveauMdp.PasswordChar = '*';
            this.txtNouveauMdp.Size = new System.Drawing.Size(200, 23);
            this.txtNouveauMdp.TabIndex = 2;
            // 
            // lblAncienMdp
            // 
            this.lblAncienMdp.AutoSize = true;
            this.lblAncienMdp.Location = new System.Drawing.Point(20, 70);
            this.lblAncienMdp.Name = "lblAncienMdp";
            this.lblAncienMdp.Size = new System.Drawing.Size(115, 20);
            this.lblAncienMdp.TabIndex = 1;
            this.lblAncienMdp.Text = "Ancien mot de passe";
            // 
            // txtAncienMdp
            // 
            this.txtAncienMdp.Location = new System.Drawing.Point(160, 70);
            this.txtAncienMdp.Name = "txtAncienMdp";
            this.txtAncienMdp.PasswordChar = '*';
            this.txtAncienMdp.Size = new System.Drawing.Size(200, 23);
            this.txtAncienMdp.TabIndex = 0;
            // 
            // btnChangerMdp
            // 
            this.btnChangerMdp.Location = new System.Drawing.Point(160, 200);
            this.btnChangerMdp.Name = "btnChangerMdp";
            this.btnChangerMdp.Size = new System.Drawing.Size(120, 35);
            this.btnChangerMdp.TabIndex = 6;
            this.btnChangerMdp.Text = "Changer";
            // 
            // FormProfil
            // 
            this.ClientSize = new System.Drawing.Size(880, 350);
            this.Controls.Add(this.groupBoxMdp);
            this.Controls.Add(this.groupBoxProfil);
            this.Text = "Mon Profil";
            this.groupBoxProfil.ResumeLayout(false);
            this.groupBoxProfil.PerformLayout();
            this.groupBoxMdp.ResumeLayout(false);
            this.groupBoxMdp.PerformLayout();
            this.ResumeLayout(false);
        }

        private MetroFramework.Controls.MetroLabel lblNom;
        private MetroFramework.Controls.MetroTextBox txtNom;
        private MetroFramework.Controls.MetroLabel lblPrenom;
        private MetroFramework.Controls.MetroTextBox txtPrenom;
        private MetroFramework.Controls.MetroLabel lblEmail;
        private MetroFramework.Controls.MetroTextBox txtEmail;
        private MetroFramework.Controls.MetroLabel lblTelephone;
        private MetroFramework.Controls.MetroTextBox txtTelephone;
        private MetroFramework.Controls.MetroLabel lblRole;
        private MetroFramework.Controls.MetroButton btnModifier;
        private System.Windows.Forms.GroupBox groupBoxProfil;
        private System.Windows.Forms.GroupBox groupBoxMdp;
        private MetroFramework.Controls.MetroLabel lblConfirmerMdp;
        private MetroFramework.Controls.MetroTextBox txtConfirmerMdp;
        private MetroFramework.Controls.MetroLabel lblNouveauMdp;
        private MetroFramework.Controls.MetroTextBox txtNouveauMdp;
        private MetroFramework.Controls.MetroLabel lblAncienMdp;
        private MetroFramework.Controls.MetroTextBox txtAncienMdp;
        private MetroFramework.Controls.MetroButton btnChangerMdp;
    }
}