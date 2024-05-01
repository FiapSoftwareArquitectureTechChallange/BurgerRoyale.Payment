using BurgerRoyale.Payment.Application.Contracts.Mappers;
using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Contracts.Validators;
using BurgerRoyale.Payment.Application.Mapper;
using BurgerRoyale.Payment.Application.UseCases;
using BurgerRoyale.Payment.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.Payment.Application.DependencyInjection;

public static class InjectionConfig
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        #region UseCases
        
        services.AddScoped<IRequestPayment, RequestPayment>();
        services.AddScoped<IGetPayment, GetPayment>();
        
        #endregion
        
        #region Mappers
        
        services.AddScoped<IPaymentMapper, PaymentMapper>();
        
        #endregion
        
        #region Validators
        
        services.AddScoped<IGetPaymentValidator, GetPaymentValidator>();
        
        #endregion
    }
}