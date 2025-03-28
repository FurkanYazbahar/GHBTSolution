using EventManagement.Application.DTOs;
using EventManagement.Application.Interfaces;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Interfaces;

namespace EventManagement.Application.Services;

public class ParticipantService : IParticipantService
{
    private readonly IParticipantRepository _participantRepo;

    public ParticipantService(IParticipantRepository participantRepo)
    {
        _participantRepo = participantRepo;
    }

    public async Task<IEnumerable<ParticipantDto>> GetAllAsync()
    {
        var participants = await _participantRepo.GetAllAsync();
        return participants.Select(p => new ParticipantDto
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Email = p.Email
        });
    }

    public async Task CreateAsync(CreateParticipantDto dto)
    {
        var participant = new Participant
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        };

        await _participantRepo.AddAsync(participant);
    }
}
