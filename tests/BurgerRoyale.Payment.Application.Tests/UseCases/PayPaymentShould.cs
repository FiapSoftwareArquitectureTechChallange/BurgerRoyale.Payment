namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Application.UseCases;
using BurgerRoyale.Payment.Domain.BackgroundMessage;
using BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;
using Microsoft.Extensions.Options;
using Moq;

internal class PayPaymentShould
{
    private Mock<IPaymentRepository> repositoryMock;
    
	private Mock<IPaymentValidator> validatorMock;

    private Mock<IOptions<MessageQueuesConfiguration>> messageQueueConfigMock;
    
	private Mock<IMessageService> messageServiceMock;
    
	private PayPayment payPayment;

    [SetUp] 
	public void SetUp()
	{
        repositoryMock = new Mock<IPaymentRepository>();

        validatorMock = new Mock<IPaymentValidator>();

		messageQueueConfigMock = new Mock<IOptions<MessageQueuesConfiguration>>();

		messageServiceMock = new Mock<IMessageService>();

        payPayment = new PayPayment(
			repositoryMock.Object,
			validatorMock.Object,
            messageQueueConfigMock.Object,
            messageServiceMock.Object);

        messageQueueConfigMock
            .Setup(queue => queue.Value)
            .Returns(new MessageQueuesConfiguration());
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
    public async Task Send_Order_Feedback_When_Pay()
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

        messageQueueConfigMock
            .Setup(queue => queue.Value)
			.Returns(new MessageQueuesConfiguration
			{
				OrderPaymentFeedbackQueue = queueName
            });

        #endregion

        #region Act(When)

        await payPayment.PayAsync(paymentId);

		#endregion

		#region Assert(Then)

		messageServiceMock
			.Verify(messageService => messageService.SendMessageAsync(
				queueName, 
				It.Is<PaymentFeedback>(model => 
					model.OrderId == payment.OrderId &&
					model.ProcessedSuccessfully == true)), 
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