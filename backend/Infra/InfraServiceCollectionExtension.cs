using Core.Application.Repositories.Contracts;
using Core.Application.Services;
using Infra.Data;
using Infra.Repositories.Impl;
using Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class InfraServiceCollectionExtension
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        
        return services;
    }
    
}