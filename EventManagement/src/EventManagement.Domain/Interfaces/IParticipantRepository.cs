using EventManagement.Domain.Entities;

namespace EventManagement.Domain.Interfaces;

public interface IParticipantRepository : IGenericRepository<Participant>
{
    Task<Participant?> GetByIdAsync(Guid id);

    Task SaveChangesAsync();
}
