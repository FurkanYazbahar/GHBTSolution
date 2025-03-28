namespace EventManagement.Domain.Entities;

public class Participant
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
}
