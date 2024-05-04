namespace BurgerRoyale.Payment.Infrastructure.Repositories;

using BurgerRoyale.Payment.Domain.Contracts.DatabaseConfiguration;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

public class PaymentRepository : IPaymentRepository
{
    private readonly IMongoCollection<Payment> _payments;
    
    public PaymentRepository(IDatabaseConfiguration databaseSettings)
    {
        MongoClient client = new(databaseSettings.ConnectionURI());
        
        IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName());

        _payments = database.GetCollection<Payment>(databaseSettings.CollectionName());
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

    public async Task<Payment?> GetById(Guid id)
    {
        return await _payments
            .Find(payment => payment.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task Update(Payment payment)
    {
        var filter = Builders<Payment>.Filter.Eq(e => e.Id, payment.Id);

        await _payments.ReplaceOneAsync(filter, payment);
    }
}