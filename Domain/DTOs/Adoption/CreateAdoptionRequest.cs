namespace Domain.DTOs.Adoption;

public class CreateAdoptionRequest
{
    public Guid PetId { get; set; }
    public Guid AdopterId { get; set; }
    public DateTime AdoptionDate { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string CounselorNotes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}