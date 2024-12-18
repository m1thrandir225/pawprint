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
    [Column("type_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetType))]
    public Guid PetTypeId { get; set; }
    public PetType PetType { get; set; } = null!;
    
    [Required]
    [Column("gender_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetGender))]
    public Guid PetGenderId { get; set; }
    public PetGender PetGender { get; set; } = null!;
    
    [Required]
    [Column("size_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetSize))]
    public Guid PetSizeId { get; set; }
    public PetSize PetSize { get; set; } = null!;
    
    [Required]
    [Column("surrender_reason_id", TypeName = "UUID")]
    [ForeignKey(nameof(SurrenderReason))]
    public Guid SurrenderReasonId { get; set; }
    public SurrenderReason SurrenderReason { get; set; } = null!;
    
    [Required]
    [Column("created_pet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Pet))]
    public Guid PetId { get; set; }
    public Pet Pet { get; set; } = null!;
    
    public ICollection<OwnerPetListingDocumets> OwnerPetListingDocuments { get; set; } = null!;
}