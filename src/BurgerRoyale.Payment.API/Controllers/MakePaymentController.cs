using BurgerRoyale.Payment.API.Extensions;
using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BurgerRoyale.Payment.API.Controllers;

[Route("api/payments")]
[ApiController]
public class MakePaymentController(IPayPayment payment) : Controller
{
    [HttpPost("{id:Guid}/pay", Name = "Pay")]
    [SwaggerOperation(
        Summary = "Pay",
        Description = "Pay for an order.")]
    [ProducesResponseType(typeof(PayPaymentResponse),
        StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails),
        StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> RequestPayment(Guid id)
    {
        PayPaymentResponse response = await payment.PayAsync(id);

        if (!response.IsValid)
        {
            return ValidationProblem(ModelState.AddErrosFromNofifications(response.Notifications));
        }

        return StatusCode(StatusCodes.Status200OK, response);
    }
}