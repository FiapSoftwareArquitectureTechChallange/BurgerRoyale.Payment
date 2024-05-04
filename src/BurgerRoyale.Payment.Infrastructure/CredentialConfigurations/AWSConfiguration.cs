using BurgerRoyale.Payment.Domain.Contracts.CredentialConfigurations;
using Microsoft.Extensions.Configuration;

namespace BurgerRoyale.Payment.Infrastructure.CredentialConfigurations;

public class AWSConfiguration(IConfiguration configuration) : ICredentialConfiguration
{
    public string AccessKey()
    {
        IConfigurationSection awsSection = GetAWSSection();
        return awsSection.GetSection("AccessKey").Value!;
    }

    private IConfigurationSection GetAWSSection()
    {
        return configuration.GetSection("AWS");
    }

    public string Region()
    {
        IConfigurationSection awsSection = GetAWSSection();
        return awsSection.GetSection("Region").Value!;
    }

    public string SecretKey()
    {
        IConfigurationSection awsSection = GetAWSSection();
        return awsSection.GetSection("SecretKey").Value!;
    }

    public string SessionToken()
    {
        IConfigurationSection awsSection = GetAWSSection();
        return awsSection.GetSection("SessionToken").Value!;
    }
}