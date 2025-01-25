using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("pets")]
public class Pet : BaseEntity
{
    [Required]
    [Column("name", TypeName = "TEXT")]
    public string Name { get; set; }

    [Column("breed", TypeName = "TEXT")]
    public string? Breed { get; set; }

    [Required]
    [Column("avatar_img", TypeName = "TEXT")]
    public string AvatarImg { get; set; }

    [Column("image_showcase", TypeName = "TEXT[]")]
    public string[] ImageShowcase { get; set; } = Array.Empty<string>();

    [Column("age_years")] public int AgeYears { get; set; }

    [Required]
    [Column("type_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetType))]
    public Guid PetTypeId { get; set; }

    public virtual PetType PetType { get; set; } = null!;

    [Required]
    [Column("gender_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetGender))]
    public Guid PetGenderId { get; set; }

    public virtual PetGender PetGender { get; set; } = null!;

    [Required]
    [Column("size_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetSize))]
    public Guid PetSizeId { get; set; }

    public virtual PetSize PetSize { get; set; } = null!;

    [Required]
    [Column("adoption_status_id", TypeName = "UUID")]
    [ForeignKey(nameof(AdoptionStatus))]
    public Guid AdoptionStatusId { get; set; }

    public virtual AdoptionStatus AdoptionStatus { get; set; } = null!;

    [Required]
    [Column("health_status_id", TypeName = "UUID")]
    [ForeignKey(nameof(HealthStatus))]
    public Guid HealthStatusId { get; set; }

    public virtual HealthStatus HealthStatus { get; set; } = null!;

    [Column("good_with_children", TypeName = "BOOLEAN")]
    public bool GoodWithChildren { get; set; } = false;

    [Column("good_with_cats", TypeName = "BOOLEAN")]
    public bool GoodWithCats { get; set; } = false;

    [Column("good_with_dogs", TypeName = "BOOLEAN")]
    public bool GoodWithDogs { get; set; } = false;

    [Column("energy_level")] public int EnergyLevel { get; set; }

    [Column("description", TypeName = "TEXT")]
    public string? Description { get; set; }

    [Column("special_requirements", TypeName = "TEXT")]
    public string? SpecialRequirements { get; set; }

    [Column("behaviorial_notes", TypeName = "TEXT")]
    public string? BehaviorialNotes { get; set; }

    [Column("intake_date", TypeName = "DATE")]
    public DateTime? IntakeDate { get; set; }


    //public virtual ICollection<OwnerPetListing> OwnerPetListings { get; set; } = new List<OwnerPetListing>();
    //public virtual ICollection<ShelterPetListing> ShelterPetListings { get; set; } = new List<ShelterPetListing>();
    //public virtual ICollection<Adoption> Adoptions { get; set; } = new List<Adoption>();

    public Pet()
    {
    }

    public Pet(
        string name,
        string avatarImg,
        Guid petTypeId,
        Guid petGenderId,
        Guid petSizeId,
        Guid adoptionStatusId,
        Guid healthStatusId,
        int ageYears,
        int energyLevel,
        bool goodWithChildren = false,
        bool goodWithCats = false,
        bool goodWithDogs = false,
        string? breed = null,
        string? description = null,
        string? specialRequirements = null,
        string? behaviorialNotes = null,
        DateTime? intakeDate = null,
        string[] imageShowcase = null)
    {
        Name = name;
        AvatarImg = avatarImg;
        PetTypeId = petTypeId;
        PetGenderId = petGenderId;
        PetSizeId = petSizeId;
        AdoptionStatusId = adoptionStatusId;
        HealthStatusId = healthStatusId;
        AgeYears = ageYears;
        EnergyLevel = energyLevel;
        GoodWithChildren = goodWithChildren;
        GoodWithCats = goodWithCats;
        GoodWithDogs = goodWithDogs;
        Breed = breed;
        Description = description;
        SpecialRequirements = specialRequirements;
        BehaviorialNotes = behaviorialNotes;
        IntakeDate = intakeDate;
        ImageShowcase = imageShowcase ?? Array.Empty<string>();
    }

}