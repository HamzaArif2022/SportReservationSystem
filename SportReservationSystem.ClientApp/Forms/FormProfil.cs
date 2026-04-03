using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp.Forms
{
    public partial class FormProfil : MetroFramework.Forms.MetroForm
    {
        private readonly ApiClient _apiClient;
        private readonly int _clientId;
        private UserDto? _user;

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
            try
            {
                var response = await _apiClient.GetAsync<AuthResponseDto>(
                    $"api/auth/user/{_clientId}");
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
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur chargement profil :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ModifierProfilAsync()
        {
            try
            {
                var ok = await _apiClient.PutAsync("api/auth/profil", new
                {
                    Nom = txtNom.Text,
                    Prenom = txtPrenom.Text,
                    Email = txtEmail.Text,
                    Telephone = txtTelephone.Text,
                });

                if (ok)
                    MessageBox.Show(this,
                        "Profil modifié avec succès.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this,
                        "Erreur lors de la modification.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task ChangerMotDePasseAsync()
        {
            if (txtNouveauMdp.Text != txtConfirmerMdp.Text)
            {
                MessageBox.Show(this,
                    "Les mots de passe ne correspondent pas.",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var ok = await _apiClient.PutAsync("api/auth/changer-mot-de-passe", new
                {
                    AncienMotDePasse = txtAncienMdp.Text,
                    NouveauMotDePasse = txtNouveauMdp.Text,
                });

                if (ok)
                {
                    MessageBox.Show(this,
                        "Mot de passe modifié avec succès.",
                        "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAncienMdp.Text = "";
                    txtNouveauMdp.Text = "";
                    txtConfirmerMdp.Text = "";
                }
                else
                {
                    MessageBox.Show(this,
                        "Erreur lors du changement de mot de passe.",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Erreur :\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}