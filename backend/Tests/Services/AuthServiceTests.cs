using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Application.Exceptions;
using Core.Application.Options;
using Core.Application.Repositories.Contracts;
using Core.Application.Services;
using Core.Application.Services.Impl;
using Core.Domain.Entities;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace backend.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
    private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;
    private readonly Mock<IOptions<JwtOptions>> _jwtOptionsMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();
        _jwtOptionsMock = new Mock<IOptions<JwtOptions>>();

        // Setup default options
        _jwtOptionsMock.Setup(x => x.Value).Returns(new JwtOptions
        {
            RefreshTokenExpireDays = 7
        });

        _authService = new AuthService(
            _userServiceMock.Object, 
            _jwtTokenGeneratorMock.Object, 
            _refreshTokenRepositoryMock.Object,
            _jwtOptionsMock.Object
        );
    }

    [Fact]
    public async Task Register_ShouldReturnAuthResponse_WhenCalled()
    {
        // Arrange
        var request = new RegisterUserRequest("test@example.com", "password123", "testuser");
        var userResponse = new UserResponse(Guid.NewGuid(), request.Email, request.Username);
        var token = "generated-token";
        var refreshToken = "generated-refresh-token";

        _userServiceMock.Setup(service => service.Register(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userResponse);

        _jwtTokenGeneratorMock.Setup(generator => generator.GenerateToken(userResponse.Id, userResponse.Email))
            .Returns(token);
        
        _jwtTokenGeneratorMock.Setup(generator => generator.GenerateRefreshToken(userResponse.Id, userResponse.Email))
            .Returns(refreshToken);
            
        _refreshTokenRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _authService.Register(request);

        // Assert
        Assert.Equal(userResponse, result.User);
        Assert.Equal(token, result.Token);
        Assert.Equal(refreshToken, result.RefreshToken);
        _userServiceMock.Verify(service => service.Register(request, It.IsAny<CancellationToken>()), Times.Once);
        _jwtTokenGeneratorMock.Verify(generator => generator.GenerateToken(userResponse.Id, userResponse.Email), Times.Once);
        _jwtTokenGeneratorMock.Verify(generator => generator.GenerateRefreshToken(userResponse.Id, userResponse.Email), Times.Once);
        _refreshTokenRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Login_ShouldReturnAuthResponse_WhenCalled()
    {
        // Arrange
        var request = new LoginRequest("test@example.com", "password123");
        var userResponse = new UserResponse(Guid.NewGuid(), request.Email, "testuser");
        var token = "generated-token";
        var refreshToken = "generated-refresh-token";

        _userServiceMock.Setup(service => service.Login(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userResponse);

        _jwtTokenGeneratorMock.Setup(generator => generator.GenerateToken(userResponse.Id, userResponse.Email))
            .Returns(token);

        _jwtTokenGeneratorMock.Setup(generator => generator.GenerateRefreshToken(userResponse.Id, userResponse.Email))
            .Returns(refreshToken);
            
        _refreshTokenRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _authService.Login(request);

        // Assert
        Assert.Equal(userResponse, result.User);
        Assert.Equal(token, result.Token);
        Assert.Equal(refreshToken, result.RefreshToken);
        _userServiceMock.Verify(service => service.Login(request, It.IsAny<CancellationToken>()), Times.Once);
        _jwtTokenGeneratorMock.Verify(generator => generator.GenerateToken(userResponse.Id, userResponse.Email), Times.Once);
        _jwtTokenGeneratorMock.Verify(generator => generator.GenerateRefreshToken(userResponse.Id, userResponse.Email), Times.Once);
        _refreshTokenRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RefreshToken_ShouldReturnAuthResponse_WhenCalled()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        var existingRefreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = "existing-refresh-token",
            UserId = userId,
            Expires = DateTime.UtcNow.AddDays(1)
        };
        var request = new LoginRequest("test@example.com", "password123");
        var userResponse = new UserResponse(userId, request.Email, "testuser");
        var newToken = "generated-token";
        var newRefreshToken = "generated-refresh-token";
        
        _userServiceMock.Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(userResponse);
        
        _refreshTokenRepositoryMock.Setup(repo => repo.GetByTokenAsync("existing-refresh-token", It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingRefreshToken);

        _jwtTokenGeneratorMock.Setup(generator => generator.GenerateToken(userResponse.Id, userResponse.Email))
            .Returns(newToken);

        _jwtTokenGeneratorMock.Setup(generator => generator.GenerateRefreshToken(userResponse.Id, userResponse.Email))
            .Returns(newRefreshToken);
            
        _refreshTokenRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RefreshToken>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        // Act
        var result = await _authService.RefreshToken("existing-refresh-token", CancellationToken.None);
        
        // Assert
        Assert.Equal(userResponse, result.User);
        Assert.Equal(newToken, result.Token);
        Assert.Equal(newRefreshToken, result.RefreshToken);
    }

    [Fact]
    public async Task RefreshToken_ShouldThrowTokenNotFoundException_WhenTokenDoesNotExist()
    {
        _refreshTokenRepositoryMock.Setup(repo => repo.GetByTokenAsync("existing-refresh-token", It.IsAny<CancellationToken>()))
            .ReturnsAsync((RefreshToken?)null);
        
        await Assert.ThrowsAsync<TokenNotFoundException>(async () => await _authService.RefreshToken("existing-refresh-token", CancellationToken.None));;
    }

    [Fact]
    public async Task RefreshToken_ShouldThrowTokenNotValidException_WhenTokenIsNotActive()
    {
        _refreshTokenRepositoryMock.Setup(repo => repo.GetByTokenAsync("existing-refresh-token", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = "existing-refresh-token",
                UserId = Guid.NewGuid(),
                Expires = DateTime.UtcNow.AddDays(-1) // Expired token
            });
        
        await Assert.ThrowsAsync<TokenNotValidException>(async () => await _authService.RefreshToken("existing-refresh-token", CancellationToken.None));;
    }
}