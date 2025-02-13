using Domain.Stripe;
using Microsoft.EntityFrameworkCore;
using Repository.Migrations;
using Stripe;

namespace Repository.Stripe;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Payment> Pay(Payment payment)
    {
        var entity = await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }
}
