using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SportReservationSystem.ClientApp;

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
        var authHeader = _httpClient.DefaultRequestHeaders.Authorization;

        var response = await _httpClient.GetAsync(endpoint);
  
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, _jsonOptions);
    }

    public async Task<bool> PutAsync(string endpoint, object? data = null)
    {
        HttpContent? content = null;
        if (data != null)
        {
            var json = JsonSerializer.Serialize(data);
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
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseJson, _jsonOptions);
    }
    
    public async Task<bool> DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);
        return response.IsSuccessStatusCode;
    }
    
    public async Task<byte[]> GetFileAsync(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync();
    }
}