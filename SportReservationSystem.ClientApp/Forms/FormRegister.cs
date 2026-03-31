using System;
using System.Windows.Forms;
using SportReservationSystem.ClientApp;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormRegister : Form
    {
        private readonly ApiClient _apiClient;
        
        // Contrôles
        private TextBox txtNom;
        private TextBox txtPrenom;
        private TextBox txtEmail;
        private TextBox txtTelephone;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Button btnRegister;
        private Button btnCancel;
        private Label lblTitle;
        private Label lblNom;
        private Label lblPrenom;
        private Label lblEmail;
        private Label lblTelephone;
        private Label lblPassword;
        private Label lblConfirmPassword;

        public FormRegister(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();
            
            btnRegister.Click += async (_, _) => await RegisterAsync();
            btnCancel.Click += (_, _) => Close();
        }

        private void InitializeComponent()
        {
            // ============================================
            // Titre
            // ============================================
            this.lblTitle = new Label();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.lblTitle.Location = new System.Drawing.Point(100, 20);
            this.lblTitle.Text = "Créer un compte";
            this.lblTitle.Size = new System.Drawing.Size(220, 32);
            
            // ============================================
            // Label Nom
            // ============================================
            this.lblNom = new Label();
            this.lblNom.AutoSize = true;
            this.lblNom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNom.Location = new System.Drawing.Point(40, 80);
            this.lblNom.Text = "Nom:";
            this.lblNom.Size = new System.Drawing.Size(38, 19);
            
            // ============================================
            // TextBox Nom
            // ============================================
            this.txtNom = new TextBox();
            this.txtNom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNom.Location = new System.Drawing.Point(140, 77);
            this.txtNom.Size = new System.Drawing.Size(220, 27);
            
            // ============================================
            // Label Prénom
            // ============================================
            this.lblPrenom = new Label();
            this.lblPrenom.AutoSize = true;
            this.lblPrenom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPrenom.Location = new System.Drawing.Point(40, 120);
            this.lblPrenom.Text = "Prénom:";
            this.lblPrenom.Size = new System.Drawing.Size(60, 19);
            
            // ============================================
            // TextBox Prénom
            // ============================================
            this.txtPrenom = new TextBox();
            this.txtPrenom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPrenom.Location = new System.Drawing.Point(140, 117);
            this.txtPrenom.Size = new System.Drawing.Size(220, 27);
            
            // ============================================
            // Label Email
            // ============================================
            this.lblEmail = new Label();
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmail.Location = new System.Drawing.Point(40, 160);
            this.lblEmail.Text = "Email:";
            this.lblEmail.Size = new System.Drawing.Size(43, 19);
            
            // ============================================
            // TextBox Email
            // ============================================
            this.txtEmail = new TextBox();
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(140, 157);
            this.txtEmail.Size = new System.Drawing.Size(220, 27);
            
            // ============================================
            // Label Téléphone
            // ============================================
            this.lblTelephone = new Label();
            this.lblTelephone.AutoSize = true;
            this.lblTelephone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTelephone.Location = new System.Drawing.Point(40, 200);
            this.lblTelephone.Text = "Téléphone:";
            this.lblTelephone.Size = new System.Drawing.Size(73, 19);
            
            // ============================================
            // TextBox Téléphone
            // ============================================
            this.txtTelephone = new TextBox();
            this.txtTelephone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTelephone.Location = new System.Drawing.Point(140, 197);
            this.txtTelephone.Size = new System.Drawing.Size(220, 27);
            
            // ============================================
            // Label Mot de passe
            // ============================================
            this.lblPassword = new Label();
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPassword.Location = new System.Drawing.Point(40, 240);
            this.lblPassword.Text = "Mot de passe:";
            this.lblPassword.Size = new System.Drawing.Size(76, 19);
            
            // ============================================
            // TextBox Mot de passe
            // ============================================
            this.txtPassword = new TextBox();
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(140, 237);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(220, 27);
            
            // ============================================
            // Label Confirmer mot de passe
            // ============================================
            this.lblConfirmPassword = new Label();
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConfirmPassword.Location = new System.Drawing.Point(40, 280);
            this.lblConfirmPassword.Text = "Confirmer:";
            this.lblConfirmPassword.Size = new System.Drawing.Size(76, 19);
            
            // ============================================
            // TextBox Confirmer mot de passe
            // ============================================
            this.txtConfirmPassword = new TextBox();
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(140, 277);
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(220, 27);
            
            // ============================================
            // Bouton S'inscrire
            // ============================================
            this.btnRegister = new Button();
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(140, 330);
            this.btnRegister.Size = new System.Drawing.Size(100, 40);
            this.btnRegister.Text = "S'inscrire";
            this.btnRegister.UseVisualStyleBackColor = false;
            
            // ============================================
            // Bouton Annuler
            // ============================================
            this.btnCancel = new Button();
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(260, 330);
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = false;
            
            // ============================================
            // Form
            // ============================================
            this.ClientSize = new System.Drawing.Size(420, 400);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.lblPrenom);
            this.Controls.Add(this.txtPrenom);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblTelephone);
            this.Controls.Add(this.txtTelephone);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnCancel);
            this.Text = "Inscription - Sport Reservation";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.White;
        }

        private async Task RegisterAsync()
        {
            // Validation des champs
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                MessageBox.Show("Veuillez saisir votre nom.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNom.Focus();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtPrenom.Text))
            {
                MessageBox.Show("Veuillez saisir votre prénom.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrenom.Focus();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Veuillez saisir votre email.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return;
            }
            
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Veuillez saisir un mot de passe.", "Information", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }
            
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas.", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            
            try
            {
                btnRegister.Enabled = false;
                btnRegister.Text = "Inscription...";
                
                var registerDto = new RegisterDto
                {
                    Nom = txtNom.Text,
                    Prenom = txtPrenom.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    Telephone = txtTelephone.Text
                };
                
                var result = await _apiClient.PostAsync<AuthResponseDto>("api/auth/register", registerDto);
                
                if (result != null && result.Success)
                {
                    MessageBox.Show(
                        "Inscription réussie !\n\n" +
                        $"Bienvenue {result.User?.Prenom} {result.User?.Nom} !\n\n" +
                        "Vous êtes maintenant connecté.", 
                        "Succès", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                    
                    // Stocker le token et l'ID utilisateur
                    _apiClient.SetToken(result.Token);
                    var clientId = result.User?.Id ?? 0;
                    
                    // Fermer le formulaire d'inscription
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    
                    // Ouvrir le formulaire principal client
                    var mainForm = new FormMainClient(_apiClient, clientId);
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show(result?.Message ?? "Erreur lors de l'inscription.", 
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRegister.Enabled = true;
                btnRegister.Text = "S'inscrire";
            }
        }
    }
}