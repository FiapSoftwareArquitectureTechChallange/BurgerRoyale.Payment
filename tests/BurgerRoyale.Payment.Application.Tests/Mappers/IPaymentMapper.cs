using BurgerRoyale.Payment.Application.Tests.UseCases;

namespace BurgerRoyale.Payment.Application.Tests.Mappers
{
    internal interface IPaymentMapper
    {
        GetPaymentResponse Map(Domain.Entities.Payment payment);
    }
}