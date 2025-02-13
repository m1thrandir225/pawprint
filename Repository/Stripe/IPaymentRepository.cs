using Domain.Stripe;
using Repository.Migrations;

namespace Repository.Stripe;

public interface IPaymentRepository
{
    Task<Payment> Pay(Payment payment);
}