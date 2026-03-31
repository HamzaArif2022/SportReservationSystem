using System;
using System.Text.Json;
using System.Windows.Forms;
using SportReservationSystem.ClientApp.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp
{
    public partial class FormLogin : Form
    {
        private readonly ApiClient _apiClient = new("http://localhost:5161");

        public FormLogin()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            try
            {
                btnLogin.Enabled = false;
                btnLogin.Text = "Connexion...";

                var result = await _apiClient.PostAsync<JsonElement>("api/auth/login", new LoginDto
                {
                    Email = txtEmail.Text,
                    Password = txtPassword.Text
                });

                if (result.ValueKind != JsonValueKind.Object)
                {
                    MessageBox.Show("Réponse login invalide.", "Erreur", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var user = result.GetProperty("user");
                var role = user.GetProperty("role").GetString() ?? string.Empty;
                
                if (!string.Equals(role, "Client", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Ce compte n'est pas un client. Veuillez utiliser l'application gestionnaire.", 
                        "Accès refusé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var token = result.GetProperty("token").GetString() ?? string.Empty;
                var clientId = user.GetProperty("id").GetInt32();

                _apiClient.SetToken(token);
                
                Hide();
                var main = new FormMainClient(_apiClient, clientId);
                main.FormClosed += (_, _) => Close();
                main.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Échec de connexion: {ex.Message}", "Erreur", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Se connecter";
            }
        }

        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            var registerForm = new FormRegister(_apiClient);
            registerForm.ShowDialog();
        }
    }
}