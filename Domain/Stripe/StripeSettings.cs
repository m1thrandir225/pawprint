namespace Domain.Stripe;

public class StripeSettings
{
    public string PublicKey { get; set; } = Environment.GetEnvironmentVariable("STRIPE_PUBLIC_KEY") ?? string.Empty;
    public string SecretKey { get; set; } = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY") ?? string.Empty;
}