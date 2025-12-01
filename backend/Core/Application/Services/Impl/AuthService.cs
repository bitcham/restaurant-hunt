using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;

namespace Core.Application.Services.Impl;

public class AuthService(IUserService userService): IAuthService
{
    public Task<UserResponse> Register(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        var savedUser = userService.Register(request, cancellationToken);

        return savedUser;
    }

    public Task<UserResponse> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var validUser = userService.Login(request, cancellationToken);
        
        return validUser;
    }
}