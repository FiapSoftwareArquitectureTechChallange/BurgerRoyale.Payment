using BurgerRoyale.Payment.Domain.Entities.Base;
using BurgerRoyale.Payment.Domain.Enums;
using Flunt.Notifications;

namespace BurgerRoyale.Payment.Domain.Entities;

public class Payment : Notifiable<Notification>, IEntityBase
{
    public Payment(Guid orderId, PaymentStatus status, decimal value)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Status = status;
        Value = value;
        Validate();
    }

    public Guid Id { get; private set; }

    public Guid OrderId { get; set; }

    public PaymentStatus Status { get; set; }

    public decimal Value { get; set; }

    private void Validate()
    {
        if (decimal.IsNegative(Value))
        {
            AddNotification("Value", "The Value cannot be negative.");
        }
    }
}