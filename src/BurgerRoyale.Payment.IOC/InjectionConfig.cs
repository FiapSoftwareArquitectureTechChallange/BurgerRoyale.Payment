using BurgerRoyale.Payment.Application.DependencyInjection;
using BurgerRoyale.Payment.BackgroundService.DependencyInjection;
using BurgerRoyale.Payment.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Payment.IOC;

public static class InjectionConfig
{
    public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDependencies();

        services.AddInfrastructureDependencies();

        services.AddAWSDependencies(configuration);

        services.AddBackgroundServiceDependencies();
    }
}