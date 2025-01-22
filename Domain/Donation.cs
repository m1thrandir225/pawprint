namespace Domain;

public class Donation
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string DonorEmail { get; set; }
    public DateTime DonationDate { get; set; }
    public string PaymentStatus { get; set; }
    public string SessionId { get; set; }

}