using Domain.enums;

namespace Domain.DTOs;

public class UpdateOwnerPetListingRequest
{
    public Guid Id { get; set; }
    public Guid AdopterId { get; set; }
    public Guid PetId { get; set; }
    public Guid SurrenderReasonId { get; set; }
    public DateTime ReviewDate { get; set; }
    public DateTime SubmissionDate { get; set; }

    public float AdoptionFee { get; set; }
    public UpdateOwnerPetListingRequest(Guid id, Guid adopterId, Guid petId, Guid surrenderReasonId, float adoptionFee)
    {
        Id = id;
        AdopterId = adopterId;
        PetId = petId;
        SurrenderReasonId = surrenderReasonId;
        AdoptionFee = adoptionFee;
    }
}