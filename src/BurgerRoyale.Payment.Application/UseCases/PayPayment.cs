namespace BurgerRoyale.Payment.Application.UseCases;

using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Extensions;

public class PayPayment(
    IPaymentRepository repository,
    IPaymentValidator validator) : IPayPayment
{
    public async Task<PayPaymentResponse> PayAsync(Guid paymentId)
    {
        Payment? payment = await GetPayment(paymentId);

        if (RequestIsInvalid(payment, out NotificationModel invalidResponse))
        {
            return invalidResponse.ConvertTo<PayPaymentResponse>();
        }

        Pay(payment!);

        Update(payment!);

        return SuccessfulResponse();
    }

    private async Task<Payment?> GetPayment(Guid paymentId)
    {
        return await repository.GetById(paymentId);
    }

    private bool RequestIsInvalid(Payment? payment, out NotificationModel invalidResponse)
    {
        return validator.IsInvalid(payment, out invalidResponse);
    }

    private static void Pay(Payment payment)
    {
        payment.Pay();
    }

    private void Update(Payment payment)
    {
        repository.Update(payment);
    }

    private static PayPaymentResponse SuccessfulResponse()
    {
        return new PayPaymentResponse();
    }
}