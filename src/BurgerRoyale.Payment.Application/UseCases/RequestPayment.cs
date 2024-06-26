﻿namespace BurgerRoyale.Payment.Application.UseCases;

using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;

public class RequestPayment(IPaymentRepository repository) : IRequestPayment
{
    public async Task<RequestPaymentResponse> RequestAsync(RequestPaymentRequest request)
    {
        Payment payment = CreatePayment(request);

        if (PaymentIsInvalid(payment))
        {
            return InvalidResponseWithNotifications(payment);
        }

        await AddPayment(payment);

        return SuccessfulResponse(payment);
    }

    private static Payment CreatePayment(RequestPaymentRequest request)
    {
        return new Payment(
            request.OrderId,
            PaymentStatus.Pending,
            request.Amount);
    }

    private static bool PaymentIsInvalid(Payment payment)
    {
        return !payment.IsValid;
    }

    private static RequestPaymentResponse InvalidResponseWithNotifications(Payment payment)
    {
        var invalidResponse = new RequestPaymentResponse();
        invalidResponse.AddNotifications(payment.Notifications);
        return invalidResponse;
    }

    private async Task AddPayment(Payment payment)
    {
        await repository.Add(payment);
    }

    private static RequestPaymentResponse SuccessfulResponse(Payment payment)
    {
        return new RequestPaymentResponse
        {
            PaymentId = payment.Id,
        };
    }
}