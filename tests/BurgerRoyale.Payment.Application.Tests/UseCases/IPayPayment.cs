
namespace BurgerRoyale.Payment.Application.Tests.UseCases
{
    internal interface IPayPayment
    {
        Task<PayPaymentResponse> PayAsync(Guid paymentId);
    }
}