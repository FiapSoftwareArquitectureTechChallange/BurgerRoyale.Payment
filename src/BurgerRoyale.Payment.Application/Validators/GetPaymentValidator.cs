namespace BurgerRoyale.Payment.Application.Validators;

using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Application.Contracts.Validators;

public class GetPaymentValidator : IGetPaymentValidator
{
    public bool IsInvalid(Payment? payment, out GetPaymentResponse response)
    {
        response = new GetPaymentResponse();

        if (PaymentDoesNotExist(payment))
        {
            response.AddNotification("PaymentId", "The payment does not exist.");
            return true;
        }

        return !response.IsValid;
    }

    private static bool PaymentDoesNotExist(Payment? payment)
    {
        return payment is null;
    }
}