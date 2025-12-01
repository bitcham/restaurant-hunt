using Core.Domain.Entities;

namespace Core.Application.Repositories.Contracts;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
}