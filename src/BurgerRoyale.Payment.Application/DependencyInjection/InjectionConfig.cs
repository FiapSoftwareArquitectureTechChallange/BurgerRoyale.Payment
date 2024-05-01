using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Payment.Application.DependencyInjection;

public static class InjectionConfig
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IRequestPayment, RequestPayment>();
    }
}