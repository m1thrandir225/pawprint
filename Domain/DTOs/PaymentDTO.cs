namespace Domain.DTOs;

public class PaymentDTO
{
    public float Amount { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
}