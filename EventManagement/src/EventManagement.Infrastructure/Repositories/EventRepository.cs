using EventManagement.Domain.Entities;
using EventManagement.Domain.Interfaces;
using EventManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrastructure.Repositories;

public class EventRepository : GenericRepository<Event>, IEventRepository
{
    private readonly EventManagementDbContext _dbContext;

    public EventRepository(EventManagementDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Events
            .Include(e => e.EventParticipants)
                .ThenInclude(ep => ep.Participant)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
