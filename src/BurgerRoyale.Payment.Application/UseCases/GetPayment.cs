namespace BurgerRoyale.Payment.Application.UseCases;

using BurgerRoyale.Payment.Application.Contracts.Mappers;
using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
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