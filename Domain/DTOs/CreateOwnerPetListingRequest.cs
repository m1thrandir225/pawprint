using Domain.enums;

namespace Domain.DTOs;

public class CreateOwnerPetListingRequest
{
    public Guid AdopterId { get; set; }
    public Guid PetId { get; set; }
    public Guid SurrenderReasonId { get; set; }
    public DateTime? ReviewDate { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public ApprovalStatus? Approved { get; set; }
}