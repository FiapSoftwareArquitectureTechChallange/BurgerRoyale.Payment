namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Domain.Entities;
using Moq;

internal class RequestPaymentShould
{
    [Test]
    public async Task Request_Payment()
    {
		#region Arrange(Given)

		var orderId = Guid.NewGuid();

		decimal value = 100;

		var request = new RequestPaymentRequest
        {
			OrderId = orderId,
			Value = value,
		};

        var paymentRepositoryMock = new Mock<IPaymentRepository>();

        IRequestPayment requestPayment = new RequestPayment(paymentRepositoryMock.Object);

        #endregion

        #region Act(When)

        RequestPaymentResponse response = await requestPayment.RequestAsync(request);

        #endregion

        #region Assert(Then)

        Assert.That(response, Is.Not.Null);

        Assert.That(response.IsValid, Is.True);

        paymentRepositoryMock
            .Verify(repository => repository.Add(It.Is<Payment>(
                payment => 
                    payment.OrderId == orderId &&
                    payment.Value == value)));

        #endregion
    }
}