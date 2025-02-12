using Domain.enums;

namespace Domain.DTOs.Adoption;

public class UpdateAdoptionRequest
{
    public DateTime? AdoptionDate { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string? CounselorNotes { get; set; }
    public Guid AdoptionStatusId { get; set; }
}