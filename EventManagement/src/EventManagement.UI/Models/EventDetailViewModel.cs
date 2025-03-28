namespace EventManagement.UI.Models;

public class EventDetailViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<ParticipantViewModel> Participants { get; set; } = new();
}

