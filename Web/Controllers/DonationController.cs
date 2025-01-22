using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository;
using Stripe;
using Stripe.Checkout;

namespace Web.Controllers;


[ApiController]
[Route("api/donations")]
public class DonationController : ControllerBase
{
    private readonly IOptions<StripeSettings> _stripeSettings;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DonationController> _logger;

    public DonationController(
        IOptions<StripeSettings> stripeSettings,
        ApplicationDbContext context,
        ILogger<DonationController> logger)
    {
        _stripeSettings = stripeSettings;
        _context = context;
        _logger = logger;
    }

    [HttpPost("create-checkout-session")]
    public async Task<ActionResult<DonationResponseDTO>> CreateCheckoutSession(
        [FromBody] CreateDonationDTO request)
    {
        try
        {
            // Create Stripe Checkout Session
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = Convert.ToInt64(request.Amount * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Donation to PetPaws Shelter",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                // MODIFY PROPER URLS 
                SuccessUrl = "http://your-frontend-url/donation/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://your-frontend-url/donation/cancel",
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            // Create pending donation record
            var donation = new Donation
            {
                Amount = request.Amount,
                DonorEmail = request.DonorEmail,
                DonationDate = DateTime.UtcNow,
                PaymentStatus = "pending",
                SessionId = session.Id
            };

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            return Ok(new DonationResponseDTO
            {
                SessionId = session.Id,
                SessionUrl = session.Url
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating checkout session");
            return StatusCode(500, "An error occurred while processing your donation");
        }
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> WebhookHandler()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _stripeSettings.Value.SecretKey
            );

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;
                var donation = await _context.Donations
                    .FirstOrDefaultAsync(d => d.SessionId == session.Id);

                if (donation != null)
                {
                    donation.PaymentStatus = "succeeded";
                    await _context.SaveChangesAsync();
                }
            }

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling webhook");
            return BadRequest();
        }
    }
}