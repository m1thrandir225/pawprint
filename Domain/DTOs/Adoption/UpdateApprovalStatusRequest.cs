using Domain.enums;

namespace Domain.DTOs.Adoption;

public class UpdateApprovalStatusRequest
{
    public ApprovalStatus Status { get; set; }
}