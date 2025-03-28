namespace EventManagement.Application.DTOs;

public class ParticipantDto
{
    public Guid Id { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
