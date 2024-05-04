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

        if (PaymentHasAlreadyBeenPaid(payment!))
        {
            response.AddNotification("Payment", "The payment has already been paid.");
            return true;
        }

        return !response.IsValid;
    }

    private bool PaymentIsInvalid(Payment? payment, out NotificationModel response)
    {
        return validator.IsInvalid(payment, out response);
    }

    private static bool PaymentHasAlreadyBeenPaid(Payment payment)
    {
        return payment.IsPaid();
    }
}