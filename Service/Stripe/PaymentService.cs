using Domain.DTOs;
using Repository.Migrations;
using Repository.Stripe;
using Domain.Stripe;
using Stripe;
using Stripe.Checkout;
using Stripe.Terminal;

namespace Service.Stripe;

public class PaymentService : IPaymentService
{
    public readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository;
        StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
    }

    public async Task<object> MakePayment(PaymentDTO dto)
    {
        try
        {
            long unitAmount = (long)(dto.Amount * 100);

            var chargeOptions = new ChargeCreateOptions
            {
                Amount = unitAmount,
                Currency = "usd",
                Source = dto.Token,
                Description = "Donation"
            };

            var chargeService = new ChargeService();
            var charge = await chargeService.CreateAsync(chargeOptions);
        
            var paymentDto = new Payment
            {
                Amount = dto.Amount,
                Email = dto.Email,
            };

            var payment = await _repository.Pay(paymentDto);

            return new
            {
                Charge = charge,
                Payment = payment
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
       
    }
}