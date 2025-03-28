using EventManagement.Application.DTOs;
using EventManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParticipantsController : ControllerBase
{
    private readonly IParticipantService _participantService;

    public ParticipantsController(IParticipantService participantService)
    {
        _participantService = participantService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParticipantDto>>> GetAll()
    {
        var participants = await _participantService.GetAllAsync();
        return Ok(participants);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateParticipantDto dto)
    {
        await _participantService.CreateAsync(dto);
        return Ok();
    }
}
