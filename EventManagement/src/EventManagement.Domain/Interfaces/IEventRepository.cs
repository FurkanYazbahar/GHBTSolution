using EventManagement.Domain.Entities;

namespace EventManagement.Domain.Interfaces;

public interface IEventRepository : IGenericRepository<Event>
{
    Task<Event?> GetByIdAsync(Guid id);
    Task SaveChangesAsync();
}
