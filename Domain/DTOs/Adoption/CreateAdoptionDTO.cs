namespace Domain.DTOs.Adoption;

public class CreateAdoptionDTO
{
    public Guid AdopterId { get; set; }
    public Guid PetId { get; set; }
    public DateTime? AdoptionDate { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string? CounselorNotes { get; set; }
}