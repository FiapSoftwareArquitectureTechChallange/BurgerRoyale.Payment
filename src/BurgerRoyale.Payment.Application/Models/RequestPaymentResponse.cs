using Flunt.Notifications;

namespace BurgerRoyale.Payment.Application.Models;

public class RequestPaymentResponse : Notifiable<Notification>
{
    public Guid PaymentId { get; set; }
}