using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;

namespace Core.Application.Services;

public interface IAuthService
{
    Task<AuthResponse> Register(RegisterUserRequest request, CancellationToken cancellationToken = default);
    Task<AuthResponse> Login(LoginRequest request, CancellationToken cancellationToken = default);
}