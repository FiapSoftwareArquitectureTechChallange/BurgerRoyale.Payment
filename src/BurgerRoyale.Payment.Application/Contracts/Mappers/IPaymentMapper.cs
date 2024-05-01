namespace BurgerRoyale.Payment.Application.Contracts.Mappers;

using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;

public interface IPaymentMapper
{
    GetPaymentResponse Map(Payment payment);
}