using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;

namespace BurgerRoyale.Payment.Application.Tests.Services;

public class OrderCompletedBackgroundService : IBackgroundService<RequestPaymentRequest>
{
    public Task ProcessMessage(RequestPaymentRequest message)
    {
        throw new NotImplementedException();
    }
}