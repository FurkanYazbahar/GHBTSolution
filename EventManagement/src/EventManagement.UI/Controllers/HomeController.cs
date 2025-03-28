using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using EventManagement.UI.Models;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("EventApi");
        var response = await client.GetAsync("events");

        var events = new List<EventListViewModel>();

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<EventListViewModel>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            events = data ?? new();
        }

        return View(events);
    }
}
