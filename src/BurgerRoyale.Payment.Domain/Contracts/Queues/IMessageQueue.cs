namespace BurgerRoyale.Payment.Domain.Contracts.Queues;

public interface IMessageQueue
{
    string OrderPaymentRequestQueue();

    string OrderPaymentFeedbackQueue();
}