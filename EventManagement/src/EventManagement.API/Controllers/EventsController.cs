using EventManagement.Application.DTOs;
using EventManagement.Application.Interfaces;
using EventManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventManagement.API.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetAll()
    {
        var events = await _eventService.GetAllAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventDto>> GetById(Guid id)
    {
        var ev = await _eventService.GetByIdAsync(id);
        if (ev == null) return NotFound();
        return Ok(ev);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateEventDto dto)
    {
        await _eventService.CreateAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _eventService.UpdateEventAsync(id, dto);
        return NoContent();
    }


    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _eventService.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("{eventId}/participant")]
    public async Task<ActionResult<IEnumerable<ParticipantDto>>> GetAllParticipantWithEventId(Guid eventId)
    {
        var participants = await _eventService.GetAllParticipantsWithEventIdAsync(eventId);        
        return Ok(participants);
    }

    [HttpPost("{eventId}/participant/{participantId}")]
    public async Task<IActionResult> AddParticipant(Guid eventId, Guid participantId)
    {
        await _eventService.AddParticipantAsync(eventId, participantId);

        return Ok();
    }

    [HttpDelete("{eventId}/participant/{participantId}")]
    public async Task<IActionResult> RemoveParticipant(Guid eventId, Guid participantId)
    {
        await _eventService.RemoveParticipantAsync(eventId, participantId);

        return Ok();
    }

}
