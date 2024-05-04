namespace BurgerRoyale.Payment.Application.Tests.Validators;

using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;

public class MakePaymentValidator : IMakePaymentValidator
{
    public bool IsInvalid(Payment? payment, out NotificationModel response)
    {
        response = new NotificationModel();

        if (payment.IsPaid())
        {
            response.AddNotification("Payment", "The payment has already been paid.");
            return true;
        }

        return !response.IsValid;
    }
}