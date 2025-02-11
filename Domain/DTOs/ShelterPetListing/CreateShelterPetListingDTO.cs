namespace Domain.DTOs.ShelterPetListing;

public class CreateShelterPetListingDTO
{
    public Guid PetId { get; set; }
    public Guid MedicalRecordId { get; set; }
    public Guid ShelterId { get; set; }
    public DateOnly? IntakeDate { get; set; }
    public float AdoptionFee { get; set; }
}