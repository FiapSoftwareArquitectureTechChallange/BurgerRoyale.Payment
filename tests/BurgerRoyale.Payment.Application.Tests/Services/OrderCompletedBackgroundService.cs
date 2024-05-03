using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;

namespace BurgerRoyale.Payment.Application.Tests.Services;

public class OrderCompletedBackgroundService(IRequestPayment requestPayment) : IBackgroundService<RequestPaymentRequest>
{
    public async Task ProcessMessage(RequestPaymentRequest message)
    {
        await requestPayment.RequestAsync(message);
    }
}