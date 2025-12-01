using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Application.Repositories.Contracts;
using Core.Domain.Entities;
using Core.Exceptions;

namespace Core.Application.Services.Impl;

public class UserService(IUserRepository userRepository) : IUserService
{
    
    
    public async Task<UserResponse> Register(RegisterUserRequest request)
    {
        if(await userRepository.GetByEmailAsync(request.Email) is not null)
        {
            throw new DuplicateEmailException();
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var user = User.Register(request.Email, passwordHash, request.Username);

        var savedUser = await userRepository.AddAsync(user);

        return UserResponse.FromEntity(savedUser);
        
    }
}