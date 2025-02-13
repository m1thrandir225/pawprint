
using Domain.DTOs;
using Domain.Stripe;

namespace Service.Stripe;

public interface IPaymentService
{
    Task<object> MakePayment(PaymentDTO dto);
}