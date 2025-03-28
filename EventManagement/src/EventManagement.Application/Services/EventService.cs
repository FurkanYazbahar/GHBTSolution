using EventManagement.Application.DTOs;
using EventManagement.Application.Interfaces;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Interfaces;

namespace EventManagement.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepo;
    private readonly IParticipantRepository _participantRepo;

    public EventService(IEventRepository eventRepo, IParticipantRepository participantRepo)
    {
        _eventRepo = eventRepo;
        _participantRepo = participantRepo;
    }

    public async Task<IEnumerable<EventDto>> GetAllAsync()
    {
        var events = await _eventRepo.GetAllAsync();
        return events.Select(e => new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Date = e.Date,
            Location = e.Location,
            Description = e.Description,
            Participants = e.EventParticipants
                .Select(ep => new ParticipantDto
                {
                    Id = ep.Participant.Id,
                    FirstName = ep.Participant.FirstName,
                    LastName = ep.Participant.LastName,
                    Email = ep.Participant.Email
                }).ToList()
        });
    }

    public async Task<EventDto?> GetByIdAsync(Guid id)
    {
        var e = await _eventRepo.GetByIdAsync(id);
        if (e == null) return null;

        return new EventDto
        {
            Id = e.Id,
            Title = e.Title,
            Date = e.Date,
            Location = e.Location,
            Description = e.Description,
            Participants = e.EventParticipants
                .Select(ep => new ParticipantDto
                {
                    Id = ep.Participant.Id,
                    FirstName = ep.Participant.FirstName,
                    LastName = ep.Participant.LastName,
                    Email = ep.Participant.Email
                }).ToList()
        };
    }

    public async Task CreateAsync(CreateEventDto dto)
    {
        var participants = new List<EventParticipant>();

        foreach (var pid in dto.ParticipantIds)
        {
            var participant = await _participantRepo.GetByIdAsync(pid);
            if (participant != null)
            {
                participants.Add(new EventParticipant { ParticipantId = pid });
            }
        }

        var newEvent = new Event
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc),
            Location = dto.Location,
            Description = dto.Description,
            EventParticipants = participants
        };

        await _eventRepo.AddAsync(newEvent);
    }

    public async Task UpdateEventAsync(Guid eventId, UpdateEventDto updateEventDto)
    {
        var entity = await _eventRepo.GetByIdAsync(eventId);
        if (entity == null) return;

        entity.Title = updateEventDto.Title;
        entity.Date = DateTime.SpecifyKind(updateEventDto.Date, DateTimeKind.Utc);
        entity.Location = updateEventDto.Location;
        entity.Description = updateEventDto.Description;

        await _eventRepo.SaveChangesAsync();
    }

    public async Task UpdateAsync(EventDto dto)
    {
         var e = await _eventRepo.GetByIdAsync(dto.Id);
        if (e == null) return;

        e.Title = dto.Title;
        e.Date = DateTime.SpecifyKind(dto.Date, DateTimeKind.Utc);
        e.Location = dto.Location;
        e.Description = dto.Description;
        e.EventParticipants = dto.Participants
            .Select(p => new EventParticipant { ParticipantId = p.Id })
            .ToList();

        await _eventRepo.UpdateAsync(e);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _eventRepo.DeleteAsync(id);
    }

    public async Task<IEnumerable<ParticipantDto>> GetAllParticipantsWithEventIdAsync(Guid eventId)
    {
        var eventEntity = await _eventRepo.GetByIdAsync(eventId);
        if (eventEntity == null) return await Task.FromResult(Enumerable.Empty<ParticipantDto>());

        var participants = eventEntity.EventParticipants
            .Select(ep => new ParticipantDto
            {
                Id = ep.Participant.Id,
                FirstName = ep.Participant.FirstName,
                LastName = ep.Participant.LastName,
                Email = ep.Participant.Email,

            });
        

        return participants;
    }

    public async Task AddParticipantAsync(Guid eventId, Guid participantId)
    {
        var eventEntity = await _eventRepo.GetByIdAsync(eventId);
        if (eventEntity == null) return;

        var alreadyExists = eventEntity.EventParticipants
            .Any(ep => ep.ParticipantId == participantId);

        if (!alreadyExists)
        {
            eventEntity.EventParticipants.Add(new EventParticipant
            {
                EventId = eventId,
                ParticipantId = participantId
            });

            await _eventRepo.SaveChangesAsync();
        }
    }

    public async Task RemoveParticipantAsync(Guid eventId, Guid participantId)
    {
        var eventEntity = await _eventRepo.GetByIdAsync(eventId);
        if (eventEntity == null) return;

        var relation = eventEntity.EventParticipants
            .FirstOrDefault(ep => ep.ParticipantId == participantId);

        if (relation != null)
        {
            eventEntity.EventParticipants.Remove(relation);
            await _eventRepo.SaveChangesAsync();
        }
    }


}
