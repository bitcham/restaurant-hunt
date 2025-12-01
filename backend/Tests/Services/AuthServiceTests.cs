using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
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
}