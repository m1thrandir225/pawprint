using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Pet : BaseEntity
{
    [Required]
    [Column("name", TypeName = "VARCHAR(255)")]
    public string Name { get; set; }

    [Required]
    [Column("pet_type_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetType))]
    public Guid PetTypeId { get; set; }
    public PetType PetType { get; set; } = null!;

    [Column("breed", TypeName = "VARCHAR(255)")]
    public string? Breed { get; set; }

    [Required]
    [Column("avatar_img", TypeName = "TEXT")]
    public string AvatarImg { get; set; }

    [Column("image_showcase", TypeName = "TEXT[]")]
    public string[] ImageShowcase { get; set; } = Array.Empty<string>();

    [Column("age_years")]
    public int AgeYears { get; set; }

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
    [Column("shelter_id", TypeName = "UUID")]
    [ForeignKey(nameof(Shelter))]
    public Guid ShelterId { get; set; }
    public Shelter Shelter { get; set; } = null!;

    [Required]
    [Column("adoption_status_id", TypeName = "UUID")]
    [ForeignKey(nameof(AdoptionStatus))]
    public Guid AdoptionStatusId { get; set; }
    public AdoptionStatus AdoptionStatus { get; set; } = null!;

    [Required]
    [Column("health_status_id", TypeName = "UUID")]
    [ForeignKey(nameof(HealthStatus))]
    public Guid HealthStatusId { get; set; }
    public HealthStatus HealthStatus { get; set; } = null!;

    [Column("medical_record_id", TypeName = "UUID")]
    [ForeignKey(nameof(MedicalRecord))]
    public Guid? MedicalRecordId { get; set; }
    public MedicalRecord? MedicalRecord { get; set; }

    // One-to-many relationship with OwnerPetListing
    public ICollection<OwnerPetListing> OwnerPetListings { get; set; } = new List<OwnerPetListing>();
}
