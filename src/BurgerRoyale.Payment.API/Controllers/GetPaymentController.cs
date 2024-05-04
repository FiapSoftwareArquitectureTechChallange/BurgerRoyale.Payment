﻿using BurgerRoyale.Payment.API.Extensions;
using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using BurgerRoyale.Payment.Domain.BackgroundMessage;
using BurgerRoyale.Payment.Domain.Contracts.IntegrationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace BurgerRoyale.Payment.API.Controllers;

[Route("api/Payment")]
[ApiController]
public class GetPaymentController(
    IGetPayment getPayment,
    IMessageService messageService,
    IOptions<MessageQueuesConfiguration> messageQueuesConfiguration) : ControllerBase
{
    [HttpGet(Name = "GetPayments")]
    [SwaggerOperation(
        Summary = "Get payments",
        Description = "Get all payments.")]
    [ProducesResponseType(typeof(IEnumerable<GetPaymentResponse>),
        StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> GetPayments()
    {
        IEnumerable<GetPaymentResponse> response = await getPayment.GetAsync();

        return Ok(response);
    }

    [HttpGet("{id:Guid}", Name = "GetPaymentById")]
    [SwaggerOperation(
        Summary = "Get payment by id",
        Description = "Get the payment given the id.")]
    [ProducesResponseType(typeof(GetPaymentResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> GetPaymentById(Guid id)
    {
        GetPaymentResponse response = await getPayment.GetByIdAsync(id);

        if (!response.IsValid)
        {
            return ValidationProblem(ModelState.AddErrosFromNofifications(response.Notifications));
        }

        return Ok(response);
    }

    [HttpGet("queues-test", Name = "Test queues")]
    [SwaggerOperation(
        Summary = "test",
        Description = "test queues.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> TestQueues()
    {
        await messageService.SendMessageAsync(
            messageQueuesConfiguration.Value.OrderPaymentRequestQueue,
            new RequestPaymentRequest
            {
                OrderId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Value = 456
            }
        );

        return Ok();
    }
}