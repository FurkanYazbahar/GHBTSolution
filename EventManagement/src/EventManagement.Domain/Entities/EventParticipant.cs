namespace EventManagement.Domain.Entities;

public class EventParticipant
{
    public Guid EventId { get; set; }
    public Event Event { get; set; } = default!;

    public Guid ParticipantId { get; set; }
    public Participant Participant { get; set; } = default!;
}
