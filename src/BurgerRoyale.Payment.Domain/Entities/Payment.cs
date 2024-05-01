using BurgerRoyale.Payment.Domain.Entities.Base;
using BurgerRoyale.Payment.Domain.Enums;

namespace BurgerRoyale.Payment.Domain.Entities;

public class Payment : EntityBase
{
    public Payment() : base()
    {
    }

    public Guid OrderId { get; set; }

    public PaymentStatus Status { get; set; }

    public decimal Value { get; set; }
}