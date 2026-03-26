using MetroFramework.Controls;

namespace SportReservationSystem.ClientApp;

partial class FormLogin
{
    private MetroTextBox _txtEmail = null!;
    private MetroTextBox _txtPassword = null!;
    private MetroButton _btnLogin = null!;

    private void InitializeComponent()
    {
        _txtEmail = new MetroTextBox();
        _txtPassword = new MetroTextBox();
        _btnLogin = new MetroButton();
        SuspendLayout();
        _txtEmail.Location = new Point(36, 74);
        _txtEmail.Size = new Size(320, 29);
        _txtEmail.WaterMark = "Email";
        _txtPassword.Location = new Point(36, 114);
        _txtPassword.PasswordChar = '*';
        _txtPassword.Size = new Size(320, 29);
        _txtPassword.WaterMark = "Mot de passe";
        _btnLogin.Location = new Point(36, 162);
        _btnLogin.Size = new Size(320, 32);
        _btnLogin.Text = "Se connecter";
        _btnLogin.UseSelectable = true;
        ClientSize = new Size(400, 250);
        Controls.Add(_btnLogin);
        Controls.Add(_txtPassword);
        Controls.Add(_txtEmail);
        Name = "FormLogin";
        Text = "Client - Connexion";
        ResumeLayout(false);
    }
}
