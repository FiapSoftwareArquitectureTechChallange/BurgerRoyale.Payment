using BurgerRoyale.Payment.Domain.Contracts.CredentialConfigurations;
using BurgerRoyale.Payment.Domain.Contracts.DatabaseConfiguration;
using BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Payment.Domain.Contracts.Queues;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Infrastructure.BackgroundMessage;
using BurgerRoyale.Payment.Infrastructure.CredentialConfigurations;
using BurgerRoyale.Payment.Infrastructure.Database.Config;
using BurgerRoyale.Payment.Infrastructure.Database.Contexts;
using BurgerRoyale.Payment.Infrastructure.QueueConfiguration;
using BurgerRoyale.Payment.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Payment.Infrastructure.DependencyInjection;

public static class InjectionConfig
{
    public static void AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddScoped<IMessageService, AWSSQSService>();

        services.AddScoped<IMessageQueue, MessageQueuesConfiguration>();

        services.AddScoped<ICredentialConfiguration, AWSConfiguration>();

        services.AddScoped<IDatabaseConfiguration, MongoDBSettings>();

        services.AddScoped<PaymentContexts>();
    }
}