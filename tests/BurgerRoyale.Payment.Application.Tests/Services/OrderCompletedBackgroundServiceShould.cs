using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;
using Moq;

namespace BurgerRoyale.Payment.Application.Tests.Services;

internal class OrderCompletedBackgroundServiceShould
{
    [Test]
    public async Task Request_A_Payment()
    {
		#region Arrange(Given)

		var orderId = Guid.NewGuid();

		decimal value = 50;

		var userId = Guid.NewGuid();

		var request = new RequestPaymentRequest
		{
			OrderId = orderId,
			Value = value,
			UserId = userId,
		};

		var requestPaymentMock = new Mock<IRequestPayment>();

		IBackgroundService<RequestPaymentRequest> backgroundService = new OrderCompletedBackgroundService();

		#endregion

		#region Act(When)

		await backgroundService.ProcessMessage(request);

        #endregion

        #region Assert(Then)

        requestPaymentMock
			.Verify(service => service.RequestAsync(request), 
			Times.Once());

        #endregion
    }
}