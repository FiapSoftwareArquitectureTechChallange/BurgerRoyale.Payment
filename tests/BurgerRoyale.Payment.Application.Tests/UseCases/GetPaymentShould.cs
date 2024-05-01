namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Application.Contracts.Mappers;
using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Application.UseCases;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;
using Moq;

internal class GetPaymentShould
{
    private Mock<IPaymentRepository> repositoryMock;
    
	private Mock<IPaymentMapper> mapperMock;
    
	private Mock<IGetPaymentValidator> validatorMock;
    
	private IGetPayment getPayment;

    [SetUp]
	public void SetUp()
	{
        repositoryMock = new Mock<IPaymentRepository>();

        mapperMock = new Mock<IPaymentMapper>();

        validatorMock = new Mock<IGetPaymentValidator>();

        getPayment = new GetPayment(
			repositoryMock.Object,
			mapperMock.Object,
            validatorMock.Object);
    }

    [Test]
    public async Task Get_Payments()
    {
		#region Arrange(Given)

		var payment1 = new Payment(Guid.NewGuid(), PaymentStatus.Paid, 10);
		
		var payment2 = new Payment(Guid.NewGuid(), PaymentStatus.Rejected, 20);

		repositoryMock
			.Setup(repository => repository.Get())
			.ReturnsAsync([payment1, payment2]);
		
		mapperMock
			.Setup(mapper => mapper.Map(payment1))
			.Returns(new GetPaymentResponse
			{
				Id = payment1.Id
			});
		
		mapperMock
			.Setup(mapper => mapper.Map(payment2))
			.Returns(new GetPaymentResponse
			{
				Id = payment2.Id
			});

		#endregion

		#region Act(When)

		IEnumerable<GetPaymentResponse> response = await getPayment.GetAsync();

		#endregion

		#region Assert(Then)

		Assert.That(response, Is.Not.Null);

		Assert.That(response, Is.Not.Empty);

		Assert.That(response.Count(), Is.EqualTo(2));

		Assert.Multiple(() =>
		{
			Assert.That(response.First().Id, Is.EqualTo(payment1.Id));

			Assert.That(response.Last().Id, Is.EqualTo(payment2.Id));
		});

		#endregion
	}
	
	[Test]
    public async Task Get_Payment_By_Id()
    {
		#region Arrange(Given)

		var paymentId = Guid.NewGuid();

        var payment = new Payment(
			paymentId, 
			Guid.NewGuid(),
			PaymentStatus.Paid,
			10);

        repositoryMock
            .Setup(repository => repository.GetById(paymentId))
			.ReturnsAsync(payment);
		
		mapperMock
			.Setup(mapper => mapper.Map(payment))
			.Returns(new GetPaymentResponse
			{
				Id = payment.Id
			});

		#endregion

		#region Act(When)

		GetPaymentResponse response = await getPayment.GetByIdAsync(paymentId);

		#endregion

		#region Assert(Then)

		Assert.That(response, Is.Not.Null);

		Assert.That(response.Id, Is.EqualTo(payment.Id));

		#endregion
	}
	
	[Test]
    public async Task Validate_When_Get_Payment_By_Id()
    {
		#region Arrange(Given)

		var paymentId = Guid.NewGuid();

		var invalidResponse = new GetPaymentResponse();

		validatorMock
			.Setup(validator => validator.IsInvalid(It.IsAny<Payment>(), out invalidResponse))
			.Returns(true);

        #endregion

        #region Act(When)

        GetPaymentResponse response = await getPayment.GetByIdAsync(paymentId);

		#endregion

		#region Assert(Then)

		Assert.That(response, Is.EqualTo(invalidResponse));

		#endregion
	}
}