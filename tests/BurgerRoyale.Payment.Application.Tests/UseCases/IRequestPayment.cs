namespace BurgerRoyale.Payment.Application.Tests.UseCases;

public interface IRequestPayment
{
    Task<RequestPaymentResponse> RequestAsync(RequestPaymentRequest request);
}