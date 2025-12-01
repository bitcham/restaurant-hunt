using Core.Application.Services;

namespace Core.Domain.Entities;

public class User : BaseEntity
{
    public Guid Id { get; set; }
    public required string Email { get; set; } = string.Empty;
    public required string PasswordHash { get; set; } = string.Empty;
    
    public required string Username { get; set; } = string.Empty;

    protected User()
    {
    }
    
    public static User Register(string email, string passwordHash, string username)
    {
         return new User
        {
            Email = email,
            PasswordHash = passwordHash,
            Username = username
        };
    }
    
    public bool VerifyPassword(string passwordHash, IPasswordHasher passwordHasher)
    {
        return passwordHasher.Verify(passwordHash, PasswordHash);
    }
    
    public void UpdateUsername(string username)
    {
        Username = username;
    }
    
    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash;
    }
    
    
}
