namespace BurgerRoyale.Payment.Infrastructure.Database.Contexts;

using BurgerRoyale.Payment.Domain.Contracts.DatabaseConfiguration;
using BurgerRoyale.Payment.Domain.Entities;
using MongoDB.Driver;

public class PaymentContexts
{
    public readonly IMongoCollection<Payment> Payments;

    public PaymentContexts(IDatabaseConfiguration databaseSettings)
    {
        MongoClient client = new(databaseSettings.ConnectionURI());

        IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName());

        Payments = database.GetCollection<Payment>(databaseSettings.CollectionName());
    }
}