namespace BurgerRoyale.Payment.Application.Tests.Validators;

using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;

public class MakePaymentValidator(IPaymentValidator validator) : IMakePaymentValidator
{
    public bool IsInvalid(Payment? payment, out NotificationModel response)
    {
        if (validator.IsInvalid(payment, out response))
        {
            return true;
        }

        if (payment.IsPaid())
        {
            response.AddNotification("Payment", "The payment has already been paid.");
            return true;
        }

        return !response.IsValid;
    }
}