using System.Windows.Forms;

namespace SportReservationSystem.ClientApp
{
    partial class FormLogin
    {
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Label lblTitle;
        private Label lblEmail;
        private Label lblPassword;

        private void InitializeComponent()
        {
            // ============================================
            // Titre
            // ============================================
            this.lblTitle = new Label();
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.lblTitle.Location = new System.Drawing.Point(80, 30);
            this.lblTitle.Text = "Sport Reservation";
            this.lblTitle.Size = new System.Drawing.Size(240, 37);
            
            // ============================================
            // Label Email
            // ============================================
            this.lblEmail = new Label();
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmail.Location = new System.Drawing.Point(50, 100);
            this.lblEmail.Text = "Email:";
            this.lblEmail.Size = new System.Drawing.Size(46, 19);
            
            // ============================================
            // TextBox Email
            // ============================================
            this.txtEmail = new TextBox();
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(120, 97);
            this.txtEmail.Size = new System.Drawing.Size(220, 27);
            
            // ============================================
            // Label Mot de passe
            // ============================================
            this.lblPassword = new Label();
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPassword.Location = new System.Drawing.Point(50, 140);
            this.lblPassword.Text = "Mot de passe:";
            this.lblPassword.Size = new System.Drawing.Size(76, 19);
            
            // ============================================
            // TextBox Mot de passe
            // ============================================
            this.txtPassword = new TextBox();
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(130, 137);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(210, 27);
            
            // ============================================
            // Bouton Se connecter
            // ============================================
            this.btnLogin = new Button();
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(120, 180);
            this.btnLogin.Size = new System.Drawing.Size(100, 35);
            this.btnLogin.Text = "Se connecter";
            this.btnLogin.UseVisualStyleBackColor = false;
            
            // ============================================
            // Bouton S'inscrire
            // ============================================
            this.btnRegister = new Button();
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(102, 102, 102);
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(230, 180);
            this.btnRegister.Size = new System.Drawing.Size(100, 35);
            this.btnRegister.Text = "S'inscrire";
            this.btnRegister.UseVisualStyleBackColor = false;
            
            // ============================================
            // Form
            // ============================================
            this.ClientSize = new System.Drawing.Size(400, 260);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnRegister);
            this.Text = "Connexion - Sport Reservation";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.White;
        }
    }
}