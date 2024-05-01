using BurgerRoyale.Payment.Application.Models;

namespace BurgerRoyale.Payment.Application.Contracts.UseCases;

public interface IGetPayment
{
    Task<IEnumerable<GetPaymentResponse>> GetAsync();
}