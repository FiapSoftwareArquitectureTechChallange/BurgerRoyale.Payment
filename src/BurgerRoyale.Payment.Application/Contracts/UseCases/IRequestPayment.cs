using BurgerRoyale.Payment.Application.Models;

namespace BurgerRoyale.Payment.Application.Contracts.UseCases;

public interface IRequestPayment
{
    Task<RequestPaymentResponse> RequestAsync(RequestPaymentRequest request);
}