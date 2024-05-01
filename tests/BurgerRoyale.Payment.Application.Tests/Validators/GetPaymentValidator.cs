using BurgerRoyale.Payment.Application.Models;

namespace BurgerRoyale.Payment.Application.Tests.Validators;

public class GetPaymentValidator : IGetPaymentValidator
{
    public bool IsInvalid(Domain.Entities.Payment? unexistingPayment, out GetPaymentResponse response)
    {
        throw new NotImplementedException();
    }
}