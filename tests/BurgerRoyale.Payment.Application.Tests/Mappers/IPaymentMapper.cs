namespace BurgerRoyale.Payment.Application.Tests.Mappers;

using BurgerRoyale.Payment.Application.Tests.UseCases;
using BurgerRoyale.Payment.Domain.Entities;

public interface IPaymentMapper
{
    GetPaymentResponse Map(Payment payment);
}