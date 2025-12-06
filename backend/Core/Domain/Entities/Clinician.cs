namespace Core.Domain.Entities;

public class Clinician : BaseEntity
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string LicenseNumber { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty; // e.g., "General Dentist", "Hygienist"
    public string? Bio { get; set; }
}
