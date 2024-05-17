using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;

public abstract class PaymentBackgroundService<TMessage> : BackgroundService, IHostedService
{
    private readonly string _queueName;

    protected IServiceProvider _serviceProvider;

    protected PaymentBackgroundService(IServiceScopeFactory serviceScopeFactory, string queueName)
    {
        _queueName = queueName;

        _serviceProvider = serviceScopeFactory
            .CreateScope()
            .ServiceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IMessageService _messageService = _serviceProvider.GetRequiredService<IMessageService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var messages = await _messageService.ReadMessagesAsync<TMessage>(_queueName, 10);

            if (messages.Any())
            {
                foreach (var msg in messages)
                {
                    await ProcessMessage(msg);
                }
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }

    protected abstract Task ProcessMessage(TMessage message);
}