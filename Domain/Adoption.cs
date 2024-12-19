using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain;

[Table("adoptions")]
public class Adoption: BaseEntity
{
    [Column("pet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Pet))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Guid PetId { get; set; }

    [Required]
    public Pet Pet { get; set; } = null!;

    [ForeignKey(nameof(Adopter))]
    [Column("adopter_id", TypeName = "UUID")]
    public Guid AdopterId { get; set; }

    [Required]
    public User Adopter { get; set; } = null!;

    [Column("adoption_date", TypeName = "DATE")]
    public DateTime AdoptionDate { get; set; }

    [Required]
    [Column("adoption_fee", TypeName = "DECIMAL")]
    public decimal AdoptionFee { get; set; }

    [Column("follow_up_date", TypeName = "DATE")]
    public DateTime? FollowUpDate { get; set; }

    [Required]
    [Column("counselor_notes", TypeName = "TEXT")]
    public string CounselorNotes { get; set; }

    [Required]
    [Column("is_successful", TypeName = "BOOLEAN")]

    public bool IsSuccessful { get; set; }

    [Required]
    [Column("created_at", TypeName = "TIMESTAMPTZ")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
