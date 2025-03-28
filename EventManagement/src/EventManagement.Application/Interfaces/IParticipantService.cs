using EventManagement.Application.DTOs;

namespace EventManagement.Application.Interfaces;

public interface IParticipantService
{
    Task<IEnumerable<ParticipantDto>> GetAllAsync();
    Task CreateAsync(CreateParticipantDto dto);
}
