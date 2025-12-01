namespace Core.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public Guid Id { get; set; }
    public required string Token { get; set; }
    public required Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public DateTime Expires { get; set; }
    public DateTime? Revoked { get; set; }
    
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => Revoked == null && !IsExpired;
}