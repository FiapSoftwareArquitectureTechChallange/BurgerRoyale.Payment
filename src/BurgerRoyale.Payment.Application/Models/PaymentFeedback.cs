namespace BurgerRoyale.Payment.Application.Models;

public class PaymentFeedback
{
    public Guid OrderId { get; set; }

    public bool ProcessedSuccessfully { get; set; }
}