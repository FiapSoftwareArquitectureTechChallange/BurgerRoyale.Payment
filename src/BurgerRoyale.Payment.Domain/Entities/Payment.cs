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
    
    public Payment(Guid id, Guid orderId, PaymentStatus status, decimal value)
    {
        Id = id;
        OrderId = orderId;
        Status = status;
        Value = value;
        Validate();
    }

    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public PaymentStatus Status { get; private set; }

    public decimal Value { get; private set; }

    private void Validate()
    {
        if (OrderIsInvalid())
        {
            AddNotification("Order", "The Order is invalid.");
        }
        
        if (StatusIsInvalid())
        {
            AddNotification("Payment Status", "The Payment Status is invalid.");
        }

        if (ValueIsNegative())
        {
            AddNotification("Value", "The Value cannot be negative.");
        }
        
        if (ValueIsNotDefined())
        {
            AddNotification("Value", "The Value is required.");
        }
    }

    private bool OrderIsInvalid()
    {
        return OrderId == Guid.Empty;
    }
    
    private bool StatusIsInvalid()
    {
        return Status == PaymentStatus.None;
    }

    private bool ValueIsNegative()
    {
        return decimal.IsNegative(Value);
    }
    
    private bool ValueIsNotDefined()
    {
        return Value == 0;
    }

    public void Pay()
    {
        Status = PaymentStatus.Paid;
    }
    public void Reject()
    {
        Status = PaymentStatus.Rejected;
    }

    public bool IsPaid()
    {
        return Status == PaymentStatus.Paid;
    }
    public bool IsPending()
    {
        return Status == PaymentStatus.Pending;
    }
}