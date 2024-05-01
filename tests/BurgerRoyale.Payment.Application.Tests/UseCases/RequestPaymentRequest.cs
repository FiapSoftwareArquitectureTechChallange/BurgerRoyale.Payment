namespace BurgerRoyale.Payment.Application.Tests.UseCases;

public class RequestPaymentRequest
{
    public Guid OrderId { get; set; }
    
    public decimal Value { get; set; }
}