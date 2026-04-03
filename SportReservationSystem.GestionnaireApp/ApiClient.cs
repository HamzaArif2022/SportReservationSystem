using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SportReservationSystem.GestionnaireApp;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    public ApiClient(string baseUrl)
    {

        if (!baseUrl.EndsWith("/"))
            baseUrl += "/";

        // pour accepter le certificat auto-signé
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        _httpClient = new HttpClient(handler) { BaseAddress = new Uri(baseUrl) };
    }

    public void SetToken(string token) => _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), _jsonOptions);
    }

    public async Task<bool> PutAsync(string endpoint, object? payload = null)
    {
        HttpContent? content = null;
        if (payload != null)
        {
            var json = JsonSerializer.Serialize(payload);
            content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        var response = await _httpClient.PutAsync(endpoint, content);
        return response.IsSuccessStatusCode;
    }

    public async Task<T?> PostAsync<T>(string endpoint, object payload)
    {
        var json = JsonSerializer.Serialize(payload);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), _jsonOptions);
    }
    
    public async Task<byte[]> GetFileAsync(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync();
    }
}
