namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;

public class RequestPayment(IPaymentRepository repository) : IRequestPayment
{
    public async Task<RequestPaymentResponse> RequestAsync(RequestPaymentRequest request)
    {
        var payment = new Payment(request.OrderId, PaymentStatus.Pending, request.Value);

        await repository.Add(payment);

        return new RequestPaymentResponse();
    }
}