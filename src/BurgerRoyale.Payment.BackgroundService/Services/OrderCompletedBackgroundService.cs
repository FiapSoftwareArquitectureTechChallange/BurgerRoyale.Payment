using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Payment.Domain.Contracts.Queues;

namespace BurgerRoyale.Payment.BackgroundService.Services;

public class OrderCompletedBackgroundService : PaymentBackgroundService<RequestPaymentRequest>
{
    private readonly IRequestPayment _requestPayment;

    public OrderCompletedBackgroundService(
        IServiceScopeFactory serviceScopeFactory,
        IServiceProvider serviceProvider) :
        base(serviceScopeFactory, GetQueueName(serviceProvider))
    {
        _requestPayment = _serviceProvider.GetRequiredService<IRequestPayment>();
    }

    private static string GetQueueName(IServiceProvider serviceProvider)
    {
        using (IServiceScope scope = serviceProvider.CreateScope())
        {

            IMessageQueue messageQueue = scope.ServiceProvider.GetRequiredService<IMessageQueue>();
            return messageQueue.OrderPaymentRequestQueue();
        }
    }

    protected override async Task ProcessMessage(RequestPaymentRequest message)
    {
        await _requestPayment.RequestAsync(message);
    }
}