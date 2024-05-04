using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.BackgroundMessage;
using BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;
using Microsoft.Extensions.Options;

namespace BurgerRoyale.Payment.API.Queue;

public class OrderCompletedBackgroundService : PaymentBackgroundService<RequestPaymentRequest>
{
    private readonly IRequestPayment requestPayment;

    public OrderCompletedBackgroundService(
        IServiceScopeFactory serviceScopeFactory, 
        IOptions<MessageQueuesConfiguration> queuesConfiguration) : 
        base(serviceScopeFactory, queuesConfiguration.Value.OrderPaymentRequestQueue)
    {
        requestPayment = _serviceProvider.GetRequiredService<IRequestPayment>();
    }

    protected override async Task ProcessMessage(RequestPaymentRequest message)
    {
        await requestPayment.RequestAsync(message);
    }
}