namespace BurgerRoyale.Payment.Domain.Contracts.Repositories;

using BurgerRoyale.Payment.Domain.Entities;

public interface IPaymentRepository
{
    Task Add(Payment payment);

    Task<IEnumerable<Payment>> Get();
}