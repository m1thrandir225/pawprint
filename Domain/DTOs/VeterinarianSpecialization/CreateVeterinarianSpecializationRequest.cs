namespace Domain.DTOs.VeterinarianSpecialization;

public class CreateVeterinarianSpecializationRequest
{
    public Guid VeterinarianId { get; set; }
    public string Specialization { get; set; } = string.Empty;
}