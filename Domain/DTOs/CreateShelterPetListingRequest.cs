namespace Domain.DTOs;

public class CreateShelterPetListingRequest
{
    public Guid PetId { get; set; }
    // public CreatePetRequest? Pet { get; set; }
    //public Guid MedicalRecordId { get; set; }
    public CreateMedicalRecordRequest? MedicalRecord { get; set; }
    public Guid ShelterId { get; set; }
    public DateOnly? IntakeDate { get; set; }
    public float AdoptionFee { get; set; }
}