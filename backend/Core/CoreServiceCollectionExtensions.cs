using Core.Application.Services;
using Core.Application.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class CoreServiceCollectionExtensions
{

    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}