namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Application.Tests.Mappers;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;
using BurgerRoyale.Payment.Domain.Enums;
using Moq;

internal class GetPaymentShould
{
    private Mock<IPaymentRepository> repositoryMock;
    
	private Mock<IPaymentMapper> mapperMock;
    
	private IGetPayment getPayment;

    [SetUp]
	public void SetUp()
	{
        repositoryMock = new Mock<IPaymentRepository>();

        mapperMock = new Mock<IPaymentMapper>();

        getPayment = new GetPayment(
			repositoryMock.Object,
			mapperMock.Object);
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
}