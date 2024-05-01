using BurgerRoyale.Payment.Domain.Enums;
using Flunt.Notifications;

namespace BurgerRoyale.Payment.Application.Models;

public class GetPaymentResponse : Notifiable<Notification>
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public PaymentStatus Status { get; set; }

    public decimal Value { get; set; }
}