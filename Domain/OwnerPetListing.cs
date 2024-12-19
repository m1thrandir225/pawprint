using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain;

public class OwnerPetListing : BaseEntity
{
    [Required]
    [Column("adopter_id", TypeName = "UUID")]
    [ForeignKey(nameof(Adopter))]
    public Guid AdopterId { get; set; }
    public Adopter Adopter { get; set; } = null!;
    
    [Required]
    [Column("pet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Pet))]
    public Guid PetId { get; set; }
    public Pet Pet { get; set; } = null!;
    
    [Required]
    [Column("surrender_reason_id", TypeName = "UUID")]
    [ForeignKey(nameof(SurrenderReason))]
    public Guid SurrenderReasonId { get; set; }
    public SurrenderReason SurrenderReason { get; set; } = null!;
    
    [Column("review_date", TypeName = "TIMESTAMPTZ")]
    public DateTime ReviewDate { get; set; } = DateTime.Now;
    [Column("submission_date", TypeName = "TIMESTAMPTZ")]
    public DateTime SubmissionDate { get; set; } = DateTime.Now;
    public virtual ICollection<OwnerPetListingDocumets> OwnerPetListingDocuments { get; set; } = null!;
}