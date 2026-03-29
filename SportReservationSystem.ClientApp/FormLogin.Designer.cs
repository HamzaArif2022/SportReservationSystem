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

        _txtEmail.Location = new Point(36, 74);
        _txtEmail.Size = new Size(320, 29);
        _txtEmail.PlaceholderText = "Email";

        _txtPassword.Location = new Point(36, 114);
        _txtPassword.Size = new Size(320, 29);
        _txtPassword.PasswordChar = '*';
        _txtPassword.PlaceholderText = "Mot de passe";

        _btnLogin.Location = new Point(36, 162);
        _btnLogin.Size = new Size(320, 32);
        _btnLogin.Text = "Se connecter";
        _btnLogin.UseVisualStyleBackColor = true;

        ClientSize = new Size(400, 250);
        Controls.Add(_btnLogin);
        Controls.Add(_txtPassword);
        Controls.Add(_txtEmail);

        Name = "FormLogin";
        Text = "Client - Connexion";
        ResumeLayout(false);
    }
}

