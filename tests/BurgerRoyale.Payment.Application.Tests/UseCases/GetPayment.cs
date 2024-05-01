using BurgerRoyale.Payment.Domain.Contracts.Repositories;

namespace BurgerRoyale.Payment.Application.Tests.UseCases;

public class GetPayment(IPaymentRepository repository) : IGetPayment
{
    public async Task<IEnumerable<GetPaymentResponse>> GetAsync()
    {
        var payments = await repository.Get();

        return payments.Select(payment => new GetPaymentResponse
        {
            Id = payment.Id,
        });
    }
}