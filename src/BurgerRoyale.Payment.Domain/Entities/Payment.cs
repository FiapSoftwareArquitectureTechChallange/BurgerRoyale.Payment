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
        if (ValueIsNegative())
        {
            AddNotification("Value", "The Value cannot be negative.");
        }
        
        if (ValueIsNotDefined())
        {
            AddNotification("Value", "The Value is required.");
        }
        
        if (StatusIsInvalid())
        {
            AddNotification("Payment Status", "The Payment Status is invalid.");
        }
    }

    private bool ValueIsNegative()
    {
        return decimal.IsNegative(Value);
    }
    
    private bool ValueIsNotDefined()
    {
        return Value == 0;
    }
    
    private bool StatusIsInvalid()
    {
        return Status == PaymentStatus.None;
    }
}