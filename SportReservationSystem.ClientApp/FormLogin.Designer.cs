using System.Drawing;
using System.Windows.Forms;

namespace SportReservationSystem.ClientApp;

partial class FormLogin
{
    private TextBox _txtEmail = null!;
    private TextBox _txtPassword = null!;
    private Button _btnLogin = null!;

    private void InitializeComponent()
    {
        _txtEmail = new TextBox();
        _txtPassword = new TextBox();
        _btnLogin = new Button();
        SuspendLayout();
        // 
        // _txtEmail
        // 
        _txtEmail.Location = new Point(36, 74);
        _txtEmail.Name = "_txtEmail";
        _txtEmail.PlaceholderText = "Email";
        _txtEmail.Size = new Size(320, 27);
        _txtEmail.TabIndex = 2;
        // 
        // _txtPassword
        // 
        _txtPassword.Location = new Point(36, 114);
        _txtPassword.Name = "_txtPassword";
        _txtPassword.PasswordChar = '*';
        _txtPassword.PlaceholderText = "Mot de passe";
        _txtPassword.Size = new Size(320, 27);
        _txtPassword.TabIndex = 1;
        // 
        // _btnLogin
        // 
        _btnLogin.Location = new Point(36, 162);
        _btnLogin.Name = "_btnLogin";
        _btnLogin.Size = new Size(320, 32);
        _btnLogin.TabIndex = 0;
        _btnLogin.Text = "Se connecter";
        _btnLogin.UseVisualStyleBackColor = true;
        // 
        // FormLogin
        // 
        ClientSize = new Size(400, 250);
        Controls.Add(_btnLogin);
        Controls.Add(_txtPassword);
        Controls.Add(_txtEmail);
        Name = "FormLogin";
        Text = "Client - Connexion";
        ResumeLayout(false);
        PerformLayout();
    }
}

