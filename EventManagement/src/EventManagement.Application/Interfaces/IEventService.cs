using EventManagement.Application.DTOs;

namespace EventManagement.Application.Interfaces;

public interface IEventService
{
    Task<IEnumerable<EventDto>> GetAllAsync();
    Task<EventDto?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateEventDto dto);
    Task UpdateAsync(EventDto dto);
    Task UpdateEventAsync(Guid id, UpdateEventDto dto); 
    Task DeleteAsync(Guid id);
    Task<IEnumerable<ParticipantDto>> GetAllParticipantsWithEventIdAsync(Guid eventId);
    Task AddParticipantAsync(Guid eventId, Guid participantId);
    Task RemoveParticipantAsync(Guid eventId, Guid participantId);
}
