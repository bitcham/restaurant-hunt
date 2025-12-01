using Core.Application.Repositories.Contracts;
using Infra.Repositories.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class InfraServiceCollectionExtension
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
    
}