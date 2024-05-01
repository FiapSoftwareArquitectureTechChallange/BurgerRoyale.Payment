namespace BurgerRoyale.Payment.Application.Contracts.Validators;

using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;

public interface IGetPaymentValidator
{
    bool IsInvalid(Payment? payment, out GetPaymentResponse response);
}