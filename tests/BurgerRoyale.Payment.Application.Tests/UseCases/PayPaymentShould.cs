namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Application.UseCases;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;
using Moq;

internal class PayPaymentShould
{
    private Mock<IPaymentRepository> repositoryMock;
    
	private Mock<IPaymentValidator> validatorMock;
    
	private PayPayment payPayment;

    [SetUp] 
	public void SetUp()
	{
        repositoryMock = new Mock<IPaymentRepository>();

        validatorMock = new Mock<IPaymentValidator>();

        payPayment = new PayPayment(
			repositoryMock.Object,
			validatorMock.Object);
    }

    [Test]
    public async Task Pay()
    {
		#region Arrange(Given)

		var paymentId = Guid.NewGuid();

		var payment = new Payment(
			paymentId,
			Guid.NewGuid(),
			PaymentStatus.Pending,
			25.99M);

		repositoryMock
			.Setup(repository => repository.GetById(paymentId))
			.ReturnsAsync(payment);

		#endregion

		#region Act(When)

		PayPaymentResponse response = await payPayment.PayAsync(paymentId);

		#endregion

		#region Assert(Then)

		Assert.That(response, Is.Not.Null);

		Assert.That(payment.Status, Is.EqualTo(PaymentStatus.Paid));

		repositoryMock
			.Verify(repository => repository.Update(payment), 
			Times.Once);

        #endregion
    }
	
	[Test]
    public async Task Validate_When_Pay()
    {
        #region Arrange(Given)

        var paymentId = Guid.NewGuid();

        Payment? unexistingPayment = null;

		var invalidResponse = new NotificationModel();
        invalidResponse.AddNotification("key", "notification");

        validatorMock
            .Setup(validator => validator.IsInvalid(unexistingPayment, out invalidResponse))
			.Returns(true);	

        #endregion

        #region Act(When)

        PayPaymentResponse response = await payPayment.PayAsync(paymentId);

		#endregion

		#region Assert(Then)

		Assert.That(response.IsValid, Is.False);

		repositoryMock
			.Verify(repository => repository.Update(It.IsAny<Payment>()), 
			Times.Never);

        #endregion
    }
}