using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.identity;

namespace Domain;

[Table("adopter_pet_type_preferences")]
public class AdopterPetTypePreference : BaseEntity
{
    [Required]
    [Column("adopter_id", TypeName = "UUID")]
    [ForeignKey(nameof(Adopter))]
    public Guid AdopterId { get; set; }

    [JsonRequired]
    public virtual User Adopter { get; set; } = null!;

    [Required]
    [Column("type_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetType))]
    public Guid PetTypeId { get; set; }
    public virtual PetType PetType { get; set; } = null!;
}