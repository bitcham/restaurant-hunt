using Core.Application.Repositories.Contracts;
using Core.Domain.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.Impl;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await dbContext.Users
            .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> AddAsync(User user)
    {
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        return user;
    }
}