namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Domain.Entities;

public interface IPaymentRepository
{
    void Add(Payment payment);
}