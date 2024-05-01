namespace BurgerRoyale.Payment.Application.Tests.Validators;

using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;

public interface IGetPaymentValidator
{
    bool IsInvalid(Payment? payment, out GetPaymentResponse response);
}