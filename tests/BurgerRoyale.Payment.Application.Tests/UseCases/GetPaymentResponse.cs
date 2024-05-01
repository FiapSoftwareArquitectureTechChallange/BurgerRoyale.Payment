using BurgerRoyale.Payment.Domain.Enums;

namespace BurgerRoyale.Payment.Application.Tests.UseCases;

public class GetPaymentResponse
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public PaymentStatus Status { get; set; }

    public decimal Value { get; set; }
}