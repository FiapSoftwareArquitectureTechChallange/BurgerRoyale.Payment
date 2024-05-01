namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Application.UseCases;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;
using Moq;

internal class RequestPaymentShould
{
    private Mock<IPaymentRepository> paymentRepositoryMock;
    
    private IRequestPayment requestPayment;
    
    private RequestPaymentRequest request;

    [SetUp]
    public void SetUp()
    {
        paymentRepositoryMock = new Mock<IPaymentRepository>();

        requestPayment = new RequestPayment(paymentRepositoryMock.Object);

        request = new RequestPaymentRequest
        {
            OrderId = Guid.NewGuid(),
            Value = 10,
        };
    }

    [Test]
    public async Task Request_Payment()
    {
		#region Arrange(Given)

		var orderId = Guid.NewGuid();

        request.OrderId = orderId;

		decimal value = 100;

        request.Value = value;

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
                    payment.Value == value)),
            Times.Once);

        #endregion
    }

    [Test]
    public async Task Create_Payment_With_Pending_Status_When_Request_Payment()
    {
		#region Arrange(Given)

        #endregion

        #region Act(When)

        await requestPayment.RequestAsync(request);

        #endregion

        #region Assert(Then)

        paymentRepositoryMock
            .Verify(repository => repository.Add(It.Is<Payment>(
                payment => 
                    payment.Status == PaymentStatus.Pending)), 
            Times.Once);

        #endregion
    }
    
    [Test]
    public async Task Validate_When_Request_Payment()
    {
        #region Arrange(Given)

        decimal invalidValue = -10;

        request.Value = invalidValue;

        #endregion

        #region Act(When)

        RequestPaymentResponse response = await requestPayment.RequestAsync(request);

        #endregion

        #region Assert(Then)

        Assert.Multiple(() =>
        {
            Assert.That(response.IsValid, Is.False);
            Assert.That(response.Notifications, Is.Not.Empty);
        });

        paymentRepositoryMock
            .Verify(repository => repository.Add(It.IsAny<Payment>()), 
            Times.Never);

        #endregion
    }
}