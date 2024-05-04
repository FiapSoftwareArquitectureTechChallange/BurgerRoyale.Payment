using BurgerRoyale.Payment.BackgroundService.Services;

namespace BurgerRoyale.Payment.BackgroundService.DependencyInjection;

public static class InjectionConfig
{
    public static void AddBackgroundServiceDependencies(this IServiceCollection services)
    {
        services.AddHostedService<OrderCompletedBackgroundService>();
    }
}