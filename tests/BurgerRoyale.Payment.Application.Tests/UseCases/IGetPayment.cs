namespace BurgerRoyale.Payment.Application.Tests.UseCases;

public interface IGetPayment
{
    Task<IEnumerable<GetPaymentResponse>> GetAsync();
}