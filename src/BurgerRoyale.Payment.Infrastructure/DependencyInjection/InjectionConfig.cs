using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Payment.Infrastructure.DependencyInjection;

public static class InjectionConfig
{
    public static void AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();
    }
}