using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Application.Options;
using Core.Application.Repositories.Contracts;
using Core.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Core.Application.Services.Impl;

public class AuthService(IUserService userService, IJwtTokenGenerator jwtTokenGenerator, IRefreshTokenRepository refreshTokenRepository, IOptions<JwtOptions> jwtOptions): IAuthService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<AuthResponse> Register(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        var savedUser = await userService.Register(request, cancellationToken);
        
        var token = jwtTokenGenerator.GenerateToken(savedUser.Id, savedUser.Email);
        var refreshTokenString = jwtTokenGenerator.GenerateRefreshToken(savedUser.Id, savedUser.Email);

        var refreshToken = new RefreshToken
        {
            Token = refreshTokenString,
            UserId = savedUser.Id,
            Expires = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpireDays)
        };

        await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

        return new AuthResponse(savedUser, token, refreshTokenString);
    }

    public async Task<AuthResponse> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var validUser = await userService.Login(request, cancellationToken);
        
        var token = jwtTokenGenerator.GenerateToken(validUser.Id, validUser.Email);
        var refreshTokenString = jwtTokenGenerator.GenerateRefreshToken(validUser.Id, validUser.Email);
        
        var refreshToken = new RefreshToken
        {
            Token = refreshTokenString,
            UserId = validUser.Id,
            Expires = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpireDays)
        };
        
        await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
        
        return new AuthResponse(validUser, token, refreshTokenString);
    }
}