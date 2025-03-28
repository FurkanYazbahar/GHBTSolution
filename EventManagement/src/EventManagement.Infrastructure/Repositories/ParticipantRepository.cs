using EventManagement.Domain.Entities;
using EventManagement.Domain.Interfaces;
using EventManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrastructure.Repositories;

public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
{
    private readonly EventManagementDbContext _dbContext;

    public ParticipantRepository(EventManagementDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public async Task<Participant?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Participants
            .Include(p => p.EventParticipants)
                .ThenInclude(ep => ep.Event)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
