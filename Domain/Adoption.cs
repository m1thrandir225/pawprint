using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.enums;

namespace Domain;

[Table("adoptions")]
public class Adoption : BaseEntity
{
    [Column("pet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Pet))]
    public Guid PetId { get; set; }

    [Required] public virtual Pet Pet { get; set; } = null!;

    [ForeignKey(nameof(Adopter))]
    [Column("adopter_id", TypeName = "UUID")]
    public Guid AdopterId { get; set; }

    [Required] public virtual User Adopter { get; set; } = null!;

    [Column("adoption_date", TypeName = "DATE")]
    public DateTime AdoptionDate { get; set; }

    

    [Column("follow_up_date", TypeName = "DATE")]
    public DateTime? FollowUpDate { get; set; }

    [Required]
    [Column("counselor_notes", TypeName = "TEXT")]
    public string CounselorNotes { get; set; }

    [Required]
    [Column("approved", TypeName = "INTEGER")]
    public virtual ApprovalStatus Approved { get; set; } = ApprovalStatus.PENDING;


    [Required]
    [Column("created_at", TypeName = "TIMESTAMPTZ")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Adoption()
    {
    }

    public Adoption(Guid petId, Guid adopterId, DateTime adoptionDate,
        DateTime? followUpDate, string counselorNotes= null)
    {
        PetId = petId;
        AdopterId = adopterId;
        AdoptionDate = adoptionDate;
        FollowUpDate = followUpDate;
        CounselorNotes = counselorNotes;
        Approved = ApprovalStatus.PENDING;
    }
}