namespace Domain.DTOs;

//veterinarian/id/spec/id
public class UpdateVeterinarianSpecializationRequest
{
    public Guid Id { get; set; }
    public Guid VeterinarianId { get; set; }
    public string Specialization { get; set; } = string.Empty;

    public UpdateVeterinarianSpecializationRequest(Guid id, Guid veterinarianId, string specialization)
    {
        Id = id;
        VeterinarianId = veterinarianId;
        Specialization = specialization;
    }
}