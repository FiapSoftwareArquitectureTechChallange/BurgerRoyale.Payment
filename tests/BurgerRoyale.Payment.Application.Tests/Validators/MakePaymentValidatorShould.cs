namespace BurgerRoyale.Payment.Application.Tests.Validators;

using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;

internal class MakePaymentValidatorShould
{
    private IMakePaymentValidator validator;

    [SetUp]
    public void SetUp()
    {
        validator = new MakePaymentValidator();
    }

    [Test]
    public void Return_Notification_When_Payment_Is_Already_Paid()
    {
        #region Arrange(Given)

        var paymentPaid = new Payment(
            Guid.NewGuid(), 
            PaymentStatus.Paid,
            10);

        #endregion

        #region Act(When)

        bool isInvalid = validator.IsInvalid(paymentPaid, out NotificationModel response);

        #endregion

        #region Assert(Then)

        Assert.That(isInvalid, Is.True);

        Assert.That(response.Notifications.Count, Is.EqualTo(1));

        Assert.Multiple(() =>
        {
            Assert.That(response.Notifications.First().Key, Is.EqualTo("Payment"));
            Assert.That(response.Notifications.First().Message, Is.EqualTo("The payment has already been paid."));
        });

        #endregion
    }
}