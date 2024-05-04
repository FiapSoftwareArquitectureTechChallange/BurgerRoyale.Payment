using BurgerRoyale.Payment.Domain.Enums;

namespace BurgerRoyale.Payment.Application.Models;

public class GetPaymentResponse : NotificationModel
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public PaymentStatus Status { get; set; }

    public decimal Value { get; set; }
}