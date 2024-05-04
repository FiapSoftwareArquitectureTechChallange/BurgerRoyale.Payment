namespace BurgerRoyale.Payment.Application.Tests.Validators;

using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;

public class MakePaymentValidator : IMakePaymentValidator
{
    public bool IsInvalid(Payment? payment, out NotificationModel response)
    {
        throw new NotImplementedException();
    }
}