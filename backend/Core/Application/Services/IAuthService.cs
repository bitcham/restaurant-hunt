using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;

namespace Core.Application.Services;

public interface IAuthService
{
    Task<UserResponse> Register(RegisterUserRequest request, CancellationToken cancellationToken = default);
    Task<UserResponse> Login(LoginRequest request, CancellationToken cancellationToken = default);
}