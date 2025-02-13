namespace Domain.Stripe;

public class Payment : BaseEntity
{
    public float Amount { get; set; }
    public string Email { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    
}