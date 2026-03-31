using System;
using System.Text.Json;
using System.Windows.Forms;
using MetroFramework.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.GestionnaireApp;

public partial class FormLogin : MetroForm
{
    private readonly ApiClient _apiClient = new("http://localhost:5161/");

    public FormLogin()
    {
        InitializeComponent();
        _btnLogin.Click += BtnLogin_Click;
    }

    private async void BtnLogin_Click(object? sender, EventArgs e)
    {
        try
        {
            var result = await _apiClient.PostAsync<JsonElement>("api/auth/login", new LoginDto
            {
                Email = _txtEmail.Text,
                Password = _txtPassword.Text
            });

            if (result.ValueKind != JsonValueKind.Object)
            {
                MessageBox.Show("Reponse login invalide.");
                return;
            }

            // Vérifier si la réponse contient un succès
            if (result.TryGetProperty("success", out var successProp) && !successProp.GetBoolean())
            {
                string message = result.GetProperty("message").GetString() ?? "Erreur de connexion";
                MessageBox.Show(message);
                return;
            }

            // Extraire l'objet user
            var user = result.GetProperty("user");
            var role = user.GetProperty("role").GetString() ?? string.Empty;
            
            // Vérifier le rôle - accepte "Gestionnaire" (et "Admin" si besoin)
            if (!string.Equals(role, "Gestionnaire", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Ce compte n'est pas un gestionnaire. Veuillez utiliser l'application client.");
                return;
            }

            var token = result.GetProperty("token").GetString() ?? string.Empty;
            _apiClient.SetToken(token);

            Hide();
            var main = new FormMainGestionnaire(_apiClient);
            main.FormClosed += (_, _) => Close();
            main.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Echec login: {ex.Message}");
        }
    }
}