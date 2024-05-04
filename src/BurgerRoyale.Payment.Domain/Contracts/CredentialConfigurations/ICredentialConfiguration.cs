namespace BurgerRoyale.Payment.Domain.Contracts.CredentialConfigurations;

public interface ICredentialConfiguration
{
    public string AccessKey();

    string SecretKey();

    string SessionToken();

    string Region();
}