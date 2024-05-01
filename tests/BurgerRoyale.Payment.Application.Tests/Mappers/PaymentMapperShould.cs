namespace BurgerRoyale.Payment.Application.Tests.Mappers;

using BurgerRoyale.Payment.Domain.Enums;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Application.Contracts.Mappers;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Application.Mapper;

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
    
    [Test]
    public void Map_Status()
    {
        #region Arrange(Given)

        var status = PaymentStatus.Paid;

        payment = new Payment(
            Guid.NewGuid(),
            status,
            0);

        #endregion

        #region Act(When)

        GetPaymentResponse paymentResponse = mapper.Map(payment);

        #endregion

        #region Assert(Then)

        Assert.That(paymentResponse.Status, Is.EqualTo(status));

        #endregion
    }
    
    [Test]
    public void Map_Value()
    {
        #region Arrange(Given)

        decimal value = 55.5M;

        payment = new Payment(
            Guid.NewGuid(),
            PaymentStatus.None,
            value);

        #endregion

        #region Act(When)

        GetPaymentResponse paymentResponse = mapper.Map(payment);

        #endregion

        #region Assert(Then)

        Assert.That(paymentResponse.Value, Is.EqualTo(value));

        #endregion
    }
}