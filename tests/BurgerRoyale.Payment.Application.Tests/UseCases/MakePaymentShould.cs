namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Application.UseCases;
using BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Payment.Domain.Contracts.Queues;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;
using Moq;

internal class MakePaymentShould
{
    private Mock<IPaymentRepository> repositoryMock;
    
	private Mock<IMakePaymentValidator> validatorMock;

    private Mock<IMessageQueue> messageQueueMock;
    
	private Mock<IMessageService> messageServiceMock;
    
	private MakePayment payPayment;

    [SetUp] 
	public void SetUp()
	{
        repositoryMock = new Mock<IPaymentRepository>();

        validatorMock = new Mock<IMakePaymentValidator>();

		messageQueueMock = new Mock<IMessageQueue>();

		messageServiceMock = new Mock<IMessageService>();

        payPayment = new MakePayment(
			repositoryMock.Object,
			validatorMock.Object,
            messageQueueMock.Object,
            messageServiceMock.Object);

        messageQueueMock
            .Setup(queue => queue.OrderPaymentFeedbackQueue())
            .Returns("");
    }

    [TestCase(true, PaymentStatus.Paid)]
    [TestCase(false, PaymentStatus.Rejected)]
    public async Task ProcessPayment(bool withSuccess, PaymentStatus expectedStatus)
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

		PayPaymentResponse response = await payPayment.ProcessPaymentAsync(paymentId, withSuccess);

		#endregion

		#region Assert(Then)

		Assert.That(response, Is.Not.Null);

		Assert.That(payment.Status, Is.EqualTo(expectedStatus));

		repositoryMock
			.Verify(repository => repository.Update(payment), 
			Times.Once);

        #endregion
    }

    [TestCase(true)]
    [TestCase(false)]
    public async Task Send_Order_Feedback_When_Pay(bool processedWithSuccess)
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


        string queueName = "sqs-order-payment-feedback";

        messageQueueMock
			.Setup(queue => queue.OrderPaymentFeedbackQueue())
			.Returns(queueName);

        #endregion

        #region Act(When)

        await payPayment.ProcessPaymentAsync(paymentId, processedWithSuccess);

		#endregion

		#region Assert(Then)

		messageServiceMock
			.Verify(messageService => messageService.SendMessageAsync(
				queueName, 
				It.Is<PaymentFeedback>(model => 
					model.OrderId == payment.OrderId &&
					model.ProcessedSuccessfully == processedWithSuccess)), 
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

        PayPaymentResponse response = await payPayment.ProcessPaymentAsync(paymentId, true);

		#endregion

		#region Assert(Then)

		Assert.That(response.IsValid, Is.False);

		repositoryMock
			.Verify(repository => repository.Update(It.IsAny<Payment>()), 
			Times.Never);

        #endregion
    }
}