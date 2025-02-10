namespace Domain.DTOs.Veterinarian;

public class CreateVeterinarianForListingRequest
{
    public string Name { get; set; }
    public string ContactNumber { get; set; }
    public string ClinicName { get; set; }
    public string Email { get; set; }
    public List<string> VeterinarianSpecializations { get; set; }
}