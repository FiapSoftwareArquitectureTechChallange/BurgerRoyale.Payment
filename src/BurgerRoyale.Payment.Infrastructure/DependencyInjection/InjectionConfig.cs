using BurgerRoyale.Payment.Domain.Contracts.DatabaseConfiguration;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Infrastructure.Database.Config;
using BurgerRoyale.Payment.Infrastructure.Database.Contexts;
using BurgerRoyale.Payment.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Payment.Infrastructure.DependencyInjection;

public static class InjectionConfig
{
    public static void AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddScoped<IDatabaseConfiguration, MongoDBSettings>();

        services.AddScoped<PaymentContexts>();
    }
}