using BurgerRoyale.Payment.Domain.Entities.Base;
using BurgerRoyale.Payment.Domain.Enums;
using Flunt.Notifications;

namespace BurgerRoyale.Payment.Domain.Entities;

public class Payment : Notifiable<Notification>, IEntityBase
{
    public Payment()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; private set; }

    public Guid OrderId { get; set; }

    public PaymentStatus Status { get; set; }

    public decimal Value { get; set; }
}