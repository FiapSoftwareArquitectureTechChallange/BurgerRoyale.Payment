namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;
using Moq;

internal class PayPaymentShould
{
    [Test]
    public async Task Pay()
    {
		#region Arrange(Given)

		var paymentId = Guid.NewGuid();

		var repositoryMock = new Mock<IPaymentRepository>();

		var payment = new Payment(
			paymentId,
			Guid.NewGuid(),
			PaymentStatus.Pending,
			25.99M);

		repositoryMock
			.Setup(repository => repository.GetById(paymentId))
			.ReturnsAsync(payment);

		IPayPayment payPayment = new PayPayment();

		#endregion

		#region Act(When)

		PayPaymentResponse response = await payPayment.PayAsync(paymentId);

		#endregion

		#region Assert(Then)

		Assert.That(response, Is.Not.Null);

		Assert.That(payment.Status, Is.EqualTo(PaymentStatus.Paid));

		repositoryMock
			.Verify(repository => repository.Save(payment));

        #endregion
    }
}