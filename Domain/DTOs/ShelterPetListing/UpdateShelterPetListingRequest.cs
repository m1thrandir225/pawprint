using Domain.enums;

namespace Domain.DTOs.ShelterPetListing;

public class UpdateShelterPetListingRequest : IUserResource
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public DateOnly? IntakeDate { get; set; }
    public ApprovalStatus Approved { get; set; }

    public float AdoptionFee { get; set; }

}