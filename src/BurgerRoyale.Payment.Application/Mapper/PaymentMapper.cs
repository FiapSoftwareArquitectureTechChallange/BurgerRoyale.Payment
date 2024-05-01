namespace BurgerRoyale.Payment.Application.Mapper;

using BurgerRoyale.Payment.Application.Contracts.Mappers;
using BurgerRoyale.Payment.Application.Models;
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