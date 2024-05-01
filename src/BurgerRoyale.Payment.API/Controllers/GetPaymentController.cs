using BurgerRoyale.Payment.Application.Contracts.UseCases;
using BurgerRoyale.Payment.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BurgerRoyale.Payment.API.Controllers;

[Route("api/Payment")]
[ApiController]
public class GetPaymentController(IGetPayment getPayment) : ControllerBase
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
}