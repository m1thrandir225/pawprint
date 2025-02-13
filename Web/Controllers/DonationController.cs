using Domain.DTOs;
using Domain.Stripe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Stripe;

namespace Web.Controllers;

[ApiController]
[Route("api/payments")]
public class DonationController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public DonationController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    
    [HttpPost]
    [Route("donation")]
    public async Task<ActionResult<object>> GetPublic([FromBody] PaymentDTO payment)
    {
        try
        {
            var result = await _paymentService.MakePayment(payment);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}