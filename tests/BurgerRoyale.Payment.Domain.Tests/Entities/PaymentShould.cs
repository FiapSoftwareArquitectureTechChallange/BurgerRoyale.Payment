namespace BurgerRoyale.Payment.Domain.Tests.Entities;

using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;

internal class PaymentShould
{
    [Test]
    public void Return_Notification_When_Have_Negative_Value()
    {
		#region Arrange(Given)

		decimal invalidValue = -10;

		#endregion

		#region Act(When)

		var payment = new Payment(
			Guid.NewGuid(),
			PaymentStatus.Pending,
			invalidValue
		);

        #endregion

        #region Assert(Then)

        Assert.Multiple(() =>
        {
            Assert.That(payment.IsValid, Is.False);

            Assert.That(payment.Notifications.Count, Is.EqualTo(1));
        });

        Assert.Multiple(() =>
		{
			Assert.That(payment.Notifications.First().Key, Is.EqualTo("Value"));
			Assert.That(payment.Notifications.First().Message, Is.EqualTo("The Value cannot be negative."));
		});

		#endregion
	}
	
	[Test]
    public void Return_Notification_When_Does_Not_Have_Value_Defined()
    {
		#region Arrange(Given)

		decimal invalidValue = 0;

		#endregion

		#region Act(When)

		var payment = new Payment(
			Guid.NewGuid(),
			PaymentStatus.Pending,
			invalidValue
		);

        #endregion

        #region Assert(Then)

        Assert.Multiple(() =>
        {
            Assert.That(payment.IsValid, Is.False);

            Assert.That(payment.Notifications.Count, Is.EqualTo(1));
        });

        Assert.Multiple(() =>
		{
			Assert.That(payment.Notifications.First().Key, Is.EqualTo("Value"));
			Assert.That(payment.Notifications.First().Message, Is.EqualTo("The Value is required."));
		});

		#endregion
	}
}