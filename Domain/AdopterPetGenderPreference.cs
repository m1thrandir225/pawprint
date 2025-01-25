using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain;

[Table("adopter_pet_gender_preferences")]
public class AdopterPetGenderPreference :BaseEntity
{
    [Required]
    [Column("adopter_id", TypeName = "UUID")]
    [ForeignKey(nameof(Adopter))]
    public Guid AdopterId { get; set; }

    [JsonIgnore]
    public virtual User Adopter { get; set; } = null!;

    [Required]
    [Column("gender_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetGender))]
    public Guid PetGenderId { get; set; }

    [Required]
    public virtual PetGender PetGender { get; set; } = null!;
}