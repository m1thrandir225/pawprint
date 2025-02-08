using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.identity;

namespace Domain;

[Table("adopter_pet_size_preferences")]
public class AdopterPetSizePreference : BaseEntity
{
    [Required]
    [Column("adopter_id", TypeName = "UUID")]
    [ForeignKey(nameof(Adopter))]
    public Guid AdopterId { get; set; }

    [JsonIgnore]
    public virtual User Adopter { get; set; } = null!;

    [Required]
    [Column("size_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetSize))]
    public Guid PetSizeId { get; set; }

    [Required]
    public virtual PetSize PetSize { get; set; } = null!;
}