namespace BurgerRoyale.Payment.Application.Models;

public class RequestPaymentRequest
{
    public Guid OrderId { get; set; }

    public decimal Value { get; set; }
}