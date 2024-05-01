namespace BurgerRoyale.Payment.Infrastructure.Repositories;

using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Infrastructure.Database.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;

public class PaymentRepository : IPaymentRepository
{
    private readonly IMongoCollection<Payment> _payments;
    
    public PaymentRepository(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new(mongoDBSettings.Value.ConnectionURI);
        
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

        _payments = database.GetCollection<Payment>(mongoDBSettings.Value.CollectionName);
    }

    public async Task Add(Payment payment)
    {
        await _payments.InsertOneAsync(payment);  
    }

    public async Task<IEnumerable<Payment>> Get()
    {
        return await _payments
            .Find(payment => true)
            .ToListAsync();
    }
}