﻿namespace BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;

public interface IMessageService
{
    Task<string> SendMessageAsync(string queueName, string message);
    
    Task<string> SendMessageAsync(string queueName, dynamic messageBody);

    Task<IEnumerable<TResponse>> ReadMessagesAsync<TResponse>(string queueName, int? maxNumberOfMessages);
}