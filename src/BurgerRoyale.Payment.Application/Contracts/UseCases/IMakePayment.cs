using BurgerRoyale.Payment.Application.Models;

namespace BurgerRoyale.Payment.Application.Contracts.UseCases;

public interface IMakePayment
{
    Task<PayPaymentResponse> ProcessPaymentAsync(Guid paymentId, bool withSuccess);
}