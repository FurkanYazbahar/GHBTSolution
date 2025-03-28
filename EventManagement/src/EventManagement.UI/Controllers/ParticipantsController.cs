using Microsoft.AspNetCore.Mvc;
using EventManagement.UI.Models;
using System.Text.Json;
using System.Text;
using EventManagement.Application.DTOs;

public class ParticipantsController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ParticipantsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var client = _httpClientFactory.CreateClient("EventApi");
        var response = await client.GetAsync("participants");

        if (!response.IsSuccessStatusCode) return BadRequest();

        var json = await response.Content.ReadAsStringAsync();
        var participants = JsonSerializer.Deserialize<List<ParticipantViewModel>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var list = participants!.Select(p => new
        {
            id = p.Id,
            fullName = $"{p.FirstName} {p.LastName}",
            email = p.Email
        });

        return Json(list);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new ParticipantCreateViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ParticipantCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var client = _httpClientFactory.CreateClient("EventApi");

        var requestBody = new
        {
            firstName = model.FirstName,
            lastName = model.LastName,
            email = model.Email
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("participants", content);

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "Katılımcı başarıyla eklendi.";
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Katılımcı eklenirken bir hata oluştu.");
        return View(model);
    }
}
