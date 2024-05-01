namespace BurgerRoyale.Payment.Application.Tests.Mappers;

using BurgerRoyale.Payment.Application.Tests.UseCases;
using BurgerRoyale.Payment.Domain.Entities;

public class PaymentMapper : IPaymentMapper
{
    public GetPaymentResponse Map(Payment payment)
    {
        return new GetPaymentResponse
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Status = payment.Status,
            Value = payment.Value,
        };
    }
}