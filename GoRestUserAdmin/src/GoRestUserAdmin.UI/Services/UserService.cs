using GoRestUserAdmin.UI.Models;
using GoRestUserAdmin.UI.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GoRestUserAdmin.UI.Services;

public class UserService : IUserService
{
    private readonly HttpClient _client;

    public UserService(IHttpClientFactory factory, IConfiguration config)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri(config["GoRest:BaseUrl"]!);
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", config["GoRest:Token"]);
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var response = await _client.GetAsync("users");
        if (!response.IsSuccessStatusCode) return new();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<UserDto>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? new();
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var response = await _client.GetAsync($"users/{id}");
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<UserDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    public async Task<bool> CreateAsync(UserDto user)
    {
        var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("users", content);
        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine("CREATE ERROR: " + error);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(UserDto user)
    {
        var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"users/{user.Id}", content);
        var body = await response.Content.ReadAsStringAsync();
        Console.WriteLine("UPDATE RESPONSE: " + body);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _client.DeleteAsync($"users/{id}");
        return response.IsSuccessStatusCode;
    }
}
