namespace BurgerRoyale.Payment.Application.Models;

public class RequestPaymentRequest
{
    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public Guid? UserId { get; set; }
}