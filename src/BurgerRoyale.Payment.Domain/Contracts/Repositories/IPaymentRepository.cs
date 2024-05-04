namespace BurgerRoyale.Payment.Domain.Contracts.Repositories;

using BurgerRoyale.Payment.Domain.Entities;
using System;

public interface IPaymentRepository
{
    Task Add(Payment payment);

    Task<IEnumerable<Payment>> Get();
    
    Task<Payment?> GetById(Guid id);

    Task Save(Payment payment);
}