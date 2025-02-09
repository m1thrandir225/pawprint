namespace Domain.DTOs.OwnerPetListing;

public class UpdateOwnerPetListingRequest : IUserResource
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public Guid AdopterId { get; set; }
    public Guid PetId { get; set; }
    public Guid SurrenderReasonId { get; set; }
    public DateTime ReviewDate { get; set; }
    public DateTime SubmissionDate { get; set; }

    public float AdoptionFee { get; set; }
}