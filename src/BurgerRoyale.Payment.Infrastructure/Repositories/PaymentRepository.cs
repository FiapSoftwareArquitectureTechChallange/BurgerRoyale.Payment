namespace BurgerRoyale.Payment.Infrastructure.Repositories;

using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Infrastructure.Database.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

public class PaymentRepository(PaymentContexts context) : IPaymentRepository
{
    public async Task Add(Payment payment)
    {
        await context.Payments.InsertOneAsync(payment);  
    }

    public async Task<IEnumerable<Payment>> Get()
    {
        return await context.Payments
            .Find(payment => true)
            .ToListAsync();
    }

    public async Task<Payment?> GetById(Guid id)
    {
        return await context.Payments
            .Find(payment => payment.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task Update(Payment payment)
    {
        var filter = Builders<Payment>.Filter.Eq(e => e.Id, payment.Id);

        await context.Payments.ReplaceOneAsync(filter, payment);
    }
}