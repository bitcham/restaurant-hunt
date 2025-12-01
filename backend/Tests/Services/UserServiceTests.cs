using Core.Application.Dtos.Requests;
using Core.Application.Exceptions;
using Core.Application.Repositories.Contracts;
using Core.Application.Services;
using Core.Application.Services.Impl;
using Core.Domain.Entities;
using Moq;
using Xunit;

namespace backend.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _userService = new UserService(_userRepositoryMock.Object, _passwordHasherMock.Object);
    }

    [Fact]
    public async Task Register_ShouldReturnUserResponse_WhenUserDoesNotExist()
    {
        // Arrange
        var request = new RegisterUserRequest("test@example.com", "password123", "testuser");
        
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        _passwordHasherMock.Setup(hasher => hasher.Hash(request.Password))
            .Returns("hashed_password");

        _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User user, CancellationToken _) => 
            {
                user.Id = Guid.NewGuid(); 
                return user;
            });

        // Act
        var result = await _userService.Register(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(request.Username, result.Username);
        Assert.NotEqual(Guid.Empty, result.Id);

        _userRepositoryMock.Verify(repo => repo.AddAsync(It.Is<User>(u => 
            u.Email == request.Email && 
            u.Username == request.Username &&
            u.PasswordHash == "hashed_password"
        ), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Register_ShouldThrowDuplicateEmailException_WhenUserAlreadyExists()
    {
        // Arrange
        var request = new RegisterUserRequest("existing@example.com", "password123", "existinguser");
        var existingUser = User.Register(request.Email, "hashed_password", request.Username);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        // Act & Assert
        await Assert.ThrowsAsync<DuplicateEmailException>(() => _userService.Register(request));

        _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Login_ShouldReturnUserResponse_WhenCredentialsAreValid()
    {
        // Arrange
        var request = new LoginRequest("test@example.com", "password123");
        var user = User.Register(request.Email, "hashed_password", "testuser");
        user.Id = Guid.NewGuid();

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasherMock.Setup(hasher => hasher.Hash(request.Password))
            .Returns("hashed_password");

        // Mock Verify indirectly: The User entity uses the hasher to verify. 
        // User.VerifyPassword calls hasher.Verify(password, hash).
        _passwordHasherMock.Setup(hasher => hasher.Verify(request.Password, "hashed_password"))
            .Returns(true);

        // Act
        var result = await _userService.Login(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Email, result.Email);
    }

    [Fact]
    public async Task Login_ShouldThrowUserNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var request = new LoginRequest("nonexistent@example.com", "password123");

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act & Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.Login(request));
    }

    [Fact]
    public async Task Login_ShouldThrowInvalidCredentialsException_WhenPasswordIsIncorrect()
    {
        // Arrange
        var request = new LoginRequest("test@example.com", "wrongpassword");
        var user = User.Register(request.Email, "hashed_password", "testuser");

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasherMock.Setup(hasher => hasher.Verify(request.Password, "hashed_password"))
            .Returns(false);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidCredentialsException>(() => _userService.Login(request));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUserResponse_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = User.Register("test@example.com", "hashed_password", "testuser");
        user.Id = userId;

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.GetByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowUserNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
            .ReturnsAsync((User?)null);

        // Act & Assert
        await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.GetByIdAsync(userId));
    }
}