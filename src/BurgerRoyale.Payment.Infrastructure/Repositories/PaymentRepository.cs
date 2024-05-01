using BurgerRoyale.Payment.Domain.Contracts.Repositories;

namespace BurgerRoyale.Payment.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    public Task Add(Domain.Entities.Payment payment)
    {
        throw new NotImplementedException();
    }
}