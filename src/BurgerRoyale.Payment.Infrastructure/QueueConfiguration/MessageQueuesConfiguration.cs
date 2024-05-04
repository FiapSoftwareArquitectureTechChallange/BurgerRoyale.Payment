using BurgerRoyale.Payment.Domain.Contracts.Queues;
using Microsoft.Extensions.Configuration;

namespace BurgerRoyale.Payment.Infrastructure.QueueConfiguration;

public class MessageQueuesConfiguration(IConfiguration configuration) : IMessageQueue
{
    public string OrderPaymentFeedbackQueue()
    {
        IConfigurationSection queueSettings = GetQueueSection();
        return queueSettings.GetSection("OrderPaymentFeedbackQueue").Value!;
    }

    private IConfigurationSection GetQueueSection()
    {
        return configuration.GetSection("MessageQueues");
    }

    public string OrderPaymentRequestQueue()
    {
        IConfigurationSection queueSettings = GetQueueSection();
        return queueSettings.GetSection("OrderPaymentRequestQueue").Value!;
    }
}