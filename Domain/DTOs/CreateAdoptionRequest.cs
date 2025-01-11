namespace Domain.DTOs;

public class CreateAdoptionRequest
{
    public Guid PetId { get; set; }
    public Guid AdopterId { get; set; }
    public DateTime AdoptionDate { get; set; }
    public decimal AdoptionFee { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string CounselorNotes { get; set; }
    public bool IsSuccessful { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}