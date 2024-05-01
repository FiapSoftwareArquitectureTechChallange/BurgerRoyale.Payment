namespace BurgerRoyale.Payment.Application.Tests.Mappers;

using BurgerRoyale.Payment.Domain.Enums;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Application.Tests.UseCases;

internal class PaymentMapperShould
{
    private IPaymentMapper mapper;
    
    private Payment payment;

    [SetUp]
    public void SetUp()
    {
        mapper = new PaymentMapper();
        
        payment = new Payment(
            Guid.NewGuid(),
            PaymentStatus.None, 
            0);
    }

    [Test]
    public void Map_Id()
    {
        #region Arrange(Given)

        Guid paymentId = payment.Id;

        #endregion

        #region Act(When)

        GetPaymentResponse paymentResponse = mapper.Map(payment);

        #endregion

        #region Assert(Then)

        Assert.That(paymentResponse, Is.Not.Null);

        Assert.That(paymentResponse.Id, Is.EqualTo(paymentId));

        #endregion
    }
    
    [Test]
    public void Map_OrderId()
    {
        #region Arrange(Given)

        var orderId = Guid.NewGuid();

        payment = new Payment(
            orderId,
            PaymentStatus.None,
            0);

        #endregion

        #region Act(When)

        GetPaymentResponse paymentResponse = mapper.Map(payment);

        #endregion

        #region Assert(Then)

        Assert.That(paymentResponse.OrderId, Is.EqualTo(orderId));

        #endregion
    }
}