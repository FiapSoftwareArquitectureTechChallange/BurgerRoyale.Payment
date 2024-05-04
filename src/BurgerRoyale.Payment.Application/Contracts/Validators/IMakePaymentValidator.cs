namespace BurgerRoyale.Payment.Application.Contracts.Validators;

using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;

public interface IMakePaymentValidator
{
    bool IsInvalid(Payment? payment, out NotificationModel response);
}