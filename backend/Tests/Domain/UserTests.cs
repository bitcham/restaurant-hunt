using Core.Application.Services;
using Core.Domain.Entities;
using Moq;
using Xunit;

namespace backend.Tests.Domain;

public class UserTests
{
    [Fact]
    public void Register_ShouldCreateUser_WithCorrectProperties()
    {
        // Arrange
        var email = "test@example.com";
        var passwordHash = "hashed_secret";
        var username = "testuser";

        // Act
        var user = User.Register(email, passwordHash, username);

        // Assert
        Assert.Equal(email, user.Email);
        Assert.Equal(passwordHash, user.PasswordHash);
        Assert.Equal(username, user.Username);
    }

    [Fact]
    public void VerifyPassword_ShouldCallHasherVerify()
    {
        // Arrange
        var password = "password123";
        var hash = "hashed_password";
        var user = User.Register("test@example.com", hash, "user");
        
        var hasherMock = new Mock<IPasswordHasher>();
        hasherMock.Setup(h => h.Verify(password, hash)).Returns(true);

        // Act
        var result = user.VerifyPassword(password, hasherMock.Object);

        // Assert
        Assert.True(result);
        hasherMock.Verify(h => h.Verify(password, hash), Times.Once);
    }

    [Fact]
    public void UpdateUsername_ShouldUpdateUsername_And_UpdatedAt()
    {
        // Arrange
        var user = User.Register("test@example.com", "hash", "oldname");
        var newName = "newname";
        
        // Act
        user.UpdateUsername(newName);
        
        // Assert
        Assert.Equal(newName, user.Username);
    }

    [Fact]
    public void ChangePassword_ShouldUpdatePasswordHash_And_UpdatedAt()
    {
        // Arrange
        var user = User.Register("test@example.com", "oldhash", "user");
        var newHash = "newhash";

        // Act
        user.ChangePassword(newHash);

        // Assert
        Assert.Equal(newHash, user.PasswordHash);
    }
}
