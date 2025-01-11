using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.enums;

namespace Domain;

public class OwnerPetListing : BaseEntity
{
    [Required]
    [Column("adopter_id", TypeName = "UUID")]
    [ForeignKey(nameof(Adopter))]
    public Guid AdopterId { get; set; }
    public virtual User Adopter { get; set; } = null!;
    
    [Required]
    [Column("pet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Pet))]
    public Guid PetId { get; set; }
    public virtual Pet Pet { get; set; } = null!;

    [Required]
    [Column("surrender_reason_id", TypeName = "UUID")]
    [ForeignKey(nameof(SurrenderReason))]
    public Guid SurrenderReasonId { get; set; }
    public virtual OwnerSurrenderReason SurrenderReason { get; set; } = null!;
    
    [Column("review_date", TypeName = "TIMESTAMPTZ")]
    public DateTime ReviewDate { get; set; } = DateTime.Now;

    [Column("submission_date", TypeName = "TIMESTAMPTZ")]
    public DateTime SubmissionDate { get; set; } = DateTime.Now;

    [Column("approved", TypeName = "INTEGER")]
    public virtual ApprovalStatus Approved { get; set; } = ApprovalStatus.PENDING;

    public virtual ICollection<OwnerPetListingDocument> OwnerPetListingDocuments { get; set; } = null!;
    
    public OwnerPetListing()
    {
    }
    
    public OwnerPetListing(Guid adopterId, Guid petId, Guid surrenderReasonId)
    {
        Id = Guid.NewGuid();
        AdopterId = adopterId;
        PetId = petId;
        SurrenderReasonId = surrenderReasonId;

    }
}