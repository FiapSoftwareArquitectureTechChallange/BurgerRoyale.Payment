namespace BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;

public interface IBackgroundService<TMessage>
{
    Task ProcessMessage(TMessage message);
}