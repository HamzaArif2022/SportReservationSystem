using System;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;
using SportReservationSystem.ClientApp.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp
{
    public partial class FormLogin : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient = new("https://localhost:7161");

        // Couleurs absentes du Designer (utilisées uniquement ici)
        private static readonly Color GoldDim = Color.FromArgb(140, 105, 15);
        private static readonly Color TextDark = Color.FromArgb(12, 12, 12);

        public FormLogin()
        {
            InitializeComponent();

            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += BtnRegister_Click;

            txtPassword.KeyDown += (_, e) =>
            {
                if (e.KeyCode == Keys.Enter) BtnLogin_Click(null, EventArgs.Empty);
            };
        }

        // ════════════════════════════════════════════════════════════════
        // CONNEXION
        // ════════════════════════════════════════════════════════════════
        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(this,
                    "Veuillez renseigner votre e-mail et votre mot de passe.",
                    "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetLoginState(loading: true);

            try
            {
                var result = await _apiClient.PostAsync<JsonElement>("api/auth/login", new LoginDto
                {
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text,
                });

                if (result.ValueKind != JsonValueKind.Object)
                {
                    MessageBox.Show(this,
                        "Réponse du serveur invalide.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var user = result.GetProperty("user");
                var role = user.GetProperty("role").GetString() ?? string.Empty;

                if (!string.Equals(role, "Client", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(this,
                        "Ce compte n'est pas un compte client.\n" +
                        "Veuillez utiliser l'application gestionnaire.",
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
                MessageBox.Show(this,
                    $"Échec de connexion :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetLoginState(loading: false);
            }
        }

        // ════════════════════════════════════════════════════════════════
        // INSCRIPTION
        // ════════════════════════════════════════════════════════════════
        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            new FormRegister(_apiClient).ShowDialog();
        }

        // ════════════════════════════════════════════════════════════════
        // HELPERS
        // ════════════════════════════════════════════════════════════════
        private void SetLoginState(bool loading)
        {
            btnLogin.Enabled = !loading;
            btnLogin.Text = loading ? "Connexion en cours..." : "Se connecter";
            btnLogin.BackColor = loading ? GoldDim : Gold;   // Gold vient du Designer
            btnLogin.ForeColor = TextDark;
            btnRegister.Enabled = !loading;
        }
    }
}