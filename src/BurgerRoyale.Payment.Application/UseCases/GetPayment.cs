namespace BurgerRoyale.Payment.Application.UseCases;

using BurgerRoyale.Payment.Application.Contracts.Mappers;
using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Extensions;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using System;

public class GetPayment(
    IPaymentRepository repository,
    IPaymentMapper mapper,
    IPaymentValidator validator) : IGetPayment
{
    public async Task<IEnumerable<GetPaymentResponse>> GetAsync()
    {
        IEnumerable<Payment> payments = await repository.Get();

        return payments.Select(mapper.Map);
    }

    public async Task<GetPaymentResponse> GetByIdAsync(Guid paymentId)
    {
        Payment? payment = await repository.GetById(paymentId);

        if (RequestIsInvalid(payment, out NotificationModel invalidResponse))
        {
            return invalidResponse.ConvertTo<GetPaymentResponse>();
        }

        return mapper.Map(payment!);
    }

    private bool RequestIsInvalid(Payment? payment, out NotificationModel invalidResponse)
    {
        return validator.IsInvalid(payment, out invalidResponse);
    }
}