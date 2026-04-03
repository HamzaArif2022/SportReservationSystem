using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormRegister : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient;

        public FormRegister(ApiClient apiClient)
        {
            _apiClient = apiClient;
            InitializeComponent();

            btnRegister.Click += async (_, _) => await RegisterAsync();
            btnCancel.Click += (_, _) => Close();
        }

        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text)) { Show("Veuillez saisir votre nom."); txtNom.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txtPrenom.Text)) { Show("Veuillez saisir votre prénom."); txtPrenom.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) { Show("Veuillez saisir votre email."); txtEmail.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txtPassword.Text)) { Show("Veuillez saisir un mot de passe."); txtPassword.Focus(); return; }
            if (txtPassword.Text != txtConfirmPassword.Text) { ShowErr("Les mots de passe ne correspondent pas."); return; }

            SetRegisterState(loading: true);
            try
            {
                var result = await _apiClient.PostAsync<AuthResponseDto>("api/auth/register", new RegisterDto
                {
                    Nom = txtNom.Text,
                    Prenom = txtPrenom.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    Telephone = txtTelephone.Text,
                });

                if (result != null && result.Success)
                {
                    MessageBox.Show(this,
                        $"Bienvenue {result.User?.Prenom} {result.User?.Nom} !\n\nVotre compte a été créé.",
                        "Inscription réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _apiClient.SetToken(result.Token);
                    var clientId = result.User?.Id ?? 0;
                    DialogResult = DialogResult.OK;
                    Close();

                    new FormMainClient(_apiClient, clientId).Show();
                }
                else
                {
                    ShowErr(result?.Message ?? "Erreur lors de l'inscription.");
                }
            }
            catch (Exception ex) { ShowErr(ex.Message); }
            finally { SetRegisterState(loading: false); }
        }

        private void SetRegisterState(bool loading)
        {
            btnRegister.Enabled = !loading;
            btnRegister.Text = loading ? "Inscription en cours..." : "Créer mon compte";
        }

        private void Show(string msg) =>
            MessageBox.Show(this, msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void ShowErr(string msg) =>
            MessageBox.Show(this, msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}