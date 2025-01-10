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
    public ApprovalStatus Approved { get; set; } = ApprovalStatus.PENDING;
    
    public UpdateOwnerPetListingRequest(Guid id, Guid adopterId, Guid petId, Guid surrenderReasonId, ApprovalStatus approved = ApprovalStatus.PENDING)
    {
        Id = id;
        AdopterId = adopterId;
        PetId = petId;
        SurrenderReasonId = surrenderReasonId;
        Approved = ApprovalStatus.PENDING;
    }
}