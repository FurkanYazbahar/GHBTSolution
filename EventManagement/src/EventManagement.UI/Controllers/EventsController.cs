using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using EventManagement.UI.Models;
using System.Net.Http.Headers;
using System.Text;

public class EventsController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public EventsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var client = _httpClientFactory.CreateClient("EventApi");
        var response = await client.GetAsync("participants");
        var participantCheckboxList = new List<ParticipantCheckboxItem>();

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var participants = JsonSerializer.Deserialize<List<ParticipantDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            participantCheckboxList = participants!.Select(p => new ParticipantCheckboxItem
            {
                Id = p.Id,
                Name = $"{p.FirstName} {p.LastName}"
            }).ToList();
        }

        return View(new EventCreateViewModel { Participants = participantCheckboxList });
    }

    [HttpPost]
    public async Task<IActionResult> Create(EventCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var client = _httpClientFactory.CreateClient("EventApi");

        var requestBody = new
        {
            title = model.Title,
            date = model.Date,
            location = model.Location,
            description = model.Description,
            participantIds = model.Participants.Where(p => p.IsSelected).Select(p => p.Id).ToList()
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("events", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Etkinlik oluşturulurken bir hata oluştu.");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var client = _httpClientFactory.CreateClient("EventApi");
        var response = await client.GetAsync($"events/{id}");

        if (!response.IsSuccessStatusCode)
            return NotFound();

        var json = await response.Content.ReadAsStringAsync();
        var dto = JsonSerializer.Deserialize<EventDetailDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var model = new EventDetailViewModel
        {
            Id = dto!.Id,
            Title = dto.Title,
            Date = dto.Date,
            Location = dto.Location,
            Description = dto.Description,
            Participants = dto.Participants.Select(p => new ParticipantViewModel
            {
                FullName = $"{p.FirstName} {p.LastName}",
                Email = p.Email
            }).ToList()
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> LoadParticipants(Guid id)
    {
        var client = _httpClientFactory.CreateClient("EventApi");
        var response = await client.GetAsync($"events/{id}");

        if (!response.IsSuccessStatusCode)
            return Content("Yüklenemedi");

        var json = await response.Content.ReadAsStringAsync();
        var dto = JsonSerializer.Deserialize<EventDetailDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var participants = dto!.Participants.Select(p => new ParticipantViewModel
        {
            Id = p.Id,
            FullName = $"{p.FirstName} {p.LastName}",
            Email = p.Email
        }).ToList();

        return PartialView("_ParticipantsPartial", participants);
    }
    // DTO sınıfı içte tanımlı
    private class EventDetailDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ParticipantDto> Participants { get; set; } = new();
    }

    private class ParticipantDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
