namespace Domain;

public class Adoption: BaseEntity
{
    // Foreign key notations is not required if it's named like this.
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public Guid AdopterId { get; set; }
    public DateTime AdoptionDate { get; set; }
    public decimal AdoptionFee { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string CounselorNotes { get; set; }
    public bool IsSuccessfull { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual Pet Pet { get; set; }
    public virtual Adopter Adopter { get; set; }
}
