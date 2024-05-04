using BurgerRoyale.Payment.Domain.Contracts.Queues;
using Microsoft.Extensions.Configuration;

namespace BurgerRoyale.Payment.Infrastructure.QueueConfiguration;

public class MessageQueuesConfiguration(IConfiguration configuration) : IMessageQueue
{
    string IMessageQueue.OrderPaymentFeedbackQueue()
    {
        IConfigurationSection queueSettings = configuration.GetSection("MessageQueues");
        return queueSettings.GetSection("OrderPaymentFeedbackQueue").Value!;
    }

    string IMessageQueue.OrderPaymentRequestQueue()
    {
        IConfigurationSection queueSettings = configuration.GetSection("MessageQueues");
        return queueSettings.GetSection("OrderPaymentRequestQueue").Value!;
    }
}