namespace BurgerRoyale.Payment.Application.Tests.Mappers;

using BurgerRoyale.Payment.Domain.Enums;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Application.Tests.UseCases;

internal class PaymentMapperShould
{
    [Test]
    public void Map_With_Id()
    {
        #region Arrange(Given)

        var payment = new Payment(Guid.NewGuid(), PaymentStatus.Paid, 10);

        IPaymentMapper mapper = new PaymentMapper();

        #endregion

        #region Act(When)

        GetPaymentResponse paymentResponse = mapper.Map(payment);

        #endregion

        #region Assert(Then)

        Assert.That(paymentResponse, Is.Not.Null);

        Assert.That(paymentResponse.Id, Is.EqualTo(payment.Id));

        #endregion
    }
}