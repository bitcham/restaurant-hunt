using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;

namespace Core.Application.Services.Impl;

public class AuthService(IUserService userService): IAuthService
{
    public Task<UserResponse> Register(RegisterUserRequest request)
    {
        var savedUser = userService.Register(request);

        return savedUser;
    }
}