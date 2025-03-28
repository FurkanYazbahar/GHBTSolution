using System.ComponentModel.DataAnnotations;

namespace EventManagement.UI.Models;

public class EventCreateViewModel
{
    [Required(ErrorMessage = "Başlık gereklidir")]
    [MinLength(3, ErrorMessage = "Başlık en az 3 karakter olmalıdır")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Tarih seçilmelidir")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Yer gereklidir")]
    public string Location { get; set; } = string.Empty;

    [Required(ErrorMessage = "Açıklama girilmelidir")]
    [MinLength(10, ErrorMessage = "Açıklama en az 10 karakter olmalıdır")]
    public string Description { get; set; } = string.Empty;

    // Katılımcılar
    public List<ParticipantCheckboxItem> Participants { get; set; } = new();
}

public class ParticipantCheckboxItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}
