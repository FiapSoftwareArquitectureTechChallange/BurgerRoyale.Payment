namespace BurgerRoyale.Payment.Application.Tests.Validators;

using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Application.Validators;
using BurgerRoyale.Payment.Domain.Entities;

internal class PaymentValidatorShould
{
    [Test]
    public void Return_Notification_When_Payment_Does_Not_Exist()
    {
		#region Arrange(Given)

		Payment? unexistingPayment = null;

        IPaymentValidator validator = new GetPaymentValidator();

        #endregion

        #region Act(When)

        bool isInvalid = validator.IsInvalid(unexistingPayment, out NotificationModel response);

        #endregion

        #region Assert(Then)

        Assert.That(isInvalid, Is.True);

        Assert.That(response.Notifications.Count, Is.EqualTo(1));

        Assert.Multiple(() =>
        {
            Assert.That(response.Notifications.First().Key, Is.EqualTo("PaymentId"));
            Assert.That(response.Notifications.First().Message, Is.EqualTo("The payment does not exist."));
        });

        #endregion
    }
}