namespace BurgerRoyale.Payment.Application.Validators;

using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;

public class PaymentValidator : IPaymentValidator
{
    public bool IsInvalid(Payment? payment, out NotificationModel response)
    {
        response = new NotificationModel();

        if (PaymentDoesNotExist(payment))
        {
            response.AddNotification("PaymentId", "The payment does not exist.");
            return true;
        }
        
        if (PaymentIsInvalid(payment!))
        {
            response.AddNotifications(payment!.Notifications);
            return true;
        }

        return !response.IsValid;
    }

    private static bool PaymentDoesNotExist(Payment? payment)
    {
        return payment is null;
    }
    
    private static bool PaymentIsInvalid(Payment payment)
    {
        return !payment.IsValid;
    }
}