using System;
using System.Text.Json;
using System.Windows.Forms;
using MetroFramework.Controls;
using MetroFramework.Forms;
using SportReservationSystem.Shared.DTOs;

namespace SportReservationSystem.ClientApp;

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

            var role = result.GetProperty("role").GetString() ?? string.Empty;
            if (!string.Equals(role, "Client", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Ce compte n'est pas un client.");
                return;
            }

            var token = result.GetProperty("token").GetString() ?? string.Empty;
            var clientId = result.GetProperty("clientId").GetInt32();
            _apiClient.SetToken(token);

            Hide();
            var main = new FormMainClient(_apiClient, clientId);
            main.FormClosed += (_, _) => Close();
            main.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Echec login: {ex.Message}");
        }
    }
}
