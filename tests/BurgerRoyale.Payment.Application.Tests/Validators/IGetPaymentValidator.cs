using BurgerRoyale.Payment.Application.Models;

namespace BurgerRoyale.Payment.Application.Tests.Validators
{
    internal interface IGetPaymentValidator
    {
        bool IsInvalid(Domain.Entities.Payment? unexistingPayment, out GetPaymentResponse response);
    }
}