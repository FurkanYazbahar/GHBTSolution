namespace EventManagement.Domain.Entities;

public class Event
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
}
