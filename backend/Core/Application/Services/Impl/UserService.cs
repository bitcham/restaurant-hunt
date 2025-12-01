using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Application.Exceptions;
using Core.Application.Repositories.Contracts;
using Core.Domain.Entities;

namespace Core.Application.Services.Impl;

public class UserService(IUserRepository userRepository, IPasswordHasher passwordHasher) : IUserService
{
    public async Task<UserResponse> Register(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        if(await userRepository.GetByEmailAsync(request.Email, cancellationToken) is not null)
        {
            throw new DuplicateEmailException();
        }

        var passwordHash = passwordHasher.Hash(request.Password);
        
        var user = User.Register(request.Email, passwordHash, request.Username);

        var savedUser = await userRepository.AddAsync(user, cancellationToken);

        return UserResponse.FromEntity(savedUser);
        
    }

    public async Task<UserResponse> Login(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, cancellationToken);
        
        if(user is null){
            throw new UserNotFoundException();
        }
        
        return !user.VerifyPassword(request.Password, passwordHasher) ? throw new InvalidCredentialsException("Username or password is incorrect.") : UserResponse.FromEntity(user);
    }

    public async Task<UserResponse> GetByIdAsync(Guid id)
    {
        return await userRepository.GetByIdAsync(id) is { } user
            ? UserResponse.FromEntity(user)
            : throw new UserNotFoundException();
    }
}