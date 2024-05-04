using BurgerRoyale.Payment.API.Extensions;
using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BurgerRoyale.Payment.API.Controllers;

[Route("api/Pay")]
[ApiController]
public class MakePaymentController(IPayPayment payment) : Controller
{
    [HttpPost("{id:Guid}", Name = "Pay")]
    [SwaggerOperation(
        Summary = "Pay",
        Description = "Pay for an order.")]
    [ProducesResponseType(typeof(RequestPaymentResponse),
        StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> RequestPayment(Guid id)
    {
        var paymentResponse = await payment.PayAsync(id);

        if (!paymentResponse.IsValid)
        {
            return ValidationProblem(ModelState.AddErrosFromNofifications(paymentResponse.Notifications));
        }

        return StatusCode(StatusCodes.Status200OK, paymentResponse);
    }
}
