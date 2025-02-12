namespace Domain.DTOs.OwnerPetListing;

public class CreateOwnerPetListingDTO
{
    public Guid AdopterId { get; set; }
    public Guid PetId { get; set; }
    public Guid SurrenderReasonId { get; set; }
    public float AdoptionFee { get; set; }
}