namespace BurgerRoyale.Payment.Application.Tests.UseCases;

using BurgerRoyale.Payment.Application.Tests.Mappers;
using BurgerRoyale.Payment.Domain.Contracts.Repositories;
using BurgerRoyale.Payment.Domain.Entities;

public class GetPayment(
    IPaymentRepository repository,
    IPaymentMapper mapper) : IGetPayment
{
    public async Task<IEnumerable<GetPaymentResponse>> GetAsync()
    {
        IEnumerable<Payment> payments = await repository.Get();

        return payments.Select(mapper.Map);
    }
}