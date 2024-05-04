using BurgerRoyale.Payment.Application.Models;

namespace BurgerRoyale.Payment.Application.Contracts.UseCases;

public interface IPayPayment
{
    Task<PayPaymentResponse> PayAsync(Guid paymentId);
}