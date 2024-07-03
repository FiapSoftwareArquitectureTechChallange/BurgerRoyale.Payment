namespace BurgerRoyale.Payment.Application.Validators;

using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;

public class MakePaymentValidator(IPaymentValidator validator) : IMakePaymentValidator
{
    public bool IsInvalid(Payment? payment, out NotificationModel response)
    {
        if (PaymentIsInvalid(payment, out response))
        {
            return true;
        }

        if (!PaymentIsPending(payment!))
        {
            response.AddNotification("Payment", "The payment is not pending.");
            return true;
        }

        return !response.IsValid;
    }

    private bool PaymentIsInvalid(Payment? payment, out NotificationModel response)
    {
        return validator.IsInvalid(payment, out response);
    }

    private static bool PaymentIsPending(Payment payment)
    {
        return payment.IsPending();
    }
}