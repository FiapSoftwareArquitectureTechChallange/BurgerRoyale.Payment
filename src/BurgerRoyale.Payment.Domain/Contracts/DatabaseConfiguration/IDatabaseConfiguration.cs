namespace BurgerRoyale.Payment.Domain.Contracts.DatabaseConfiguration;

public interface IDatabaseConfiguration
{
    string ConnectionURI();

    string DatabaseName();

    string CollectionName();
}