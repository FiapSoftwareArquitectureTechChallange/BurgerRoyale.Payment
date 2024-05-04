namespace BurgerRoyale.Payment.Application.UseCases;

using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;

public class PayPayment(IPaymentRepository repository) : IPayPayment
{
    public async Task<PayPaymentResponse> PayAsync(Guid paymentId)
    {
        Payment? payment = await repository.GetById(paymentId);
        payment.Pay();
        repository.Update(payment);
        return new PayPaymentResponse();
    }
}