using BurgerRoyale.Payment.Application.DependencyInjection;
using BurgerRoyale.Payment.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Payment.IOC;

public static class InjectionConfig
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddApplicationDependencies();

        services.AddInfrastructureDependencies();
    }
}