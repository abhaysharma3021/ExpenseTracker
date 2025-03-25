using ExpenseTracker.Application.Contracts;
using ExpenseTracker.Application.Mappings;
using ExpenseTracker.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExpenseTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAutoMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(UserMappingProfile), 
            typeof(ExpenseMappingProfile),
            typeof(CategoryMappingProfile)
        );

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
