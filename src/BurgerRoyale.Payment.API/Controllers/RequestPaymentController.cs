using BurgerRoyale.Payment.API.Extensions;
using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BurgerRoyale.Payment.API.Controllers;

[Route("api/Payment")]
[ApiController]
public class RequestPaymentController(IRequestPayment requestPayment) : ControllerBase
{
    [HttpPost(Name = "RequestPayment")]
    [SwaggerOperation(
        Summary = "Request a payment", 
        Description = "Request payment of a specific order.")]
    [ProducesResponseType(typeof(RequestPaymentResponse),
        StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> RequestPayment([FromBody] RequestPaymentRequest request)
    {
        RequestPaymentResponse response = await requestPayment.RequestAsync(request);

        if (!response.IsValid)
        {
            return ValidationProblem(ModelState.AddErrosFromNofifications(response.Notifications));
        }

        return StatusCode(StatusCodes.Status201Created, response);
    }
}