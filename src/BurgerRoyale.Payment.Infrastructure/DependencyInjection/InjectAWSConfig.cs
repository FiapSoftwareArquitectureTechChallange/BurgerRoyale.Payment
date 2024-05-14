using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SQS;
using BurgerRoyale.Payment.Domain.Contracts.CredentialConfigurations;
using BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Payment.Domain.Contracts.Queues;
using BurgerRoyale.Payment.Infrastructure.BackgroundMessage;
using BurgerRoyale.Payment.Infrastructure.CredentialConfigurations;
using BurgerRoyale.Payment.Infrastructure.QueueConfiguration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Payment.Infrastructure.DependencyInjection;

public static class InjectAWSConfig
{
    public static void AddAWSDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMessageService, AWSSQSService>();

        services.AddScoped<IMessageQueue, MessageQueuesConfiguration>();

        services.AddScoped<ICredentialConfiguration, AWSConfiguration>();

        var awsConfiguration = configuration
            .GetSection("AWS");

        services.AddDefaultAWSOptions(new AWSOptions()
        {
            Credentials = new SessionAWSCredentials(
                    awsConfiguration.GetSection("AccessKey").Value,
                    awsConfiguration.GetSection("SecretKey").Value,
                    awsConfiguration.GetSection("SessionToken").Value
                ),

            Region = RegionEndpoint.GetBySystemName(awsConfiguration.GetSection("Region").Value)
        });

        services.AddAWSService<IAmazonSQS>(ServiceLifetime.Scoped);
    }
}