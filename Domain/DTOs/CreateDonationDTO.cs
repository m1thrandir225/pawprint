namespace Domain.DTOs;

public class CreateDonationDTO
{
    public decimal Amount { get; set; }
    public string DonorEmail { get; set; }
}