namespace BurgerRoyale.Payment.Application.Tests.Validators;

using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Application.Validators;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;
using Moq;

internal class MakePaymentValidatorShould
{
    private Mock<IPaymentValidator> paymentValidatorMock;
    
    private IMakePaymentValidator validator;

    [SetUp]
    public void SetUp()
    {
        paymentValidatorMock = new Mock<IPaymentValidator>();

        validator = new MakePaymentValidator(paymentValidatorMock.Object);
    }

    [Test]
    public void Return_Notification_When_Payment_Is_Already_Paid()
    {
        #region Arrange(Given)

        var paymentPaid = new Payment(
            Guid.NewGuid(), 
            PaymentStatus.Paid,
            10);

        var responseModel = new NotificationModel();

        paymentValidatorMock
            .Setup(paymentValidator => paymentValidator.IsInvalid(paymentPaid, out responseModel))
            .Callback(() => responseModel = new NotificationModel());

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
    
    [Test]
    public void Validate_Payment()
    {
        #region Arrange(Given)

        Payment? unexistingPayment = null;

        var invalidResponse = new NotificationModel();

        paymentValidatorMock
            .Setup(paymentValidator => paymentValidator.IsInvalid(unexistingPayment, out invalidResponse))
            .Returns(true);

        #endregion

        #region Act(When)

        bool isInvalid = validator.IsInvalid(unexistingPayment, out NotificationModel response);

        #endregion

        #region Assert(Then)

        Assert.That(isInvalid, Is.True);

        Assert.That(response, Is.EqualTo(invalidResponse));

        #endregion
    }
}