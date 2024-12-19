using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class AdopterPetTypePreference
{
    [Required]
    [Column("adopter_id", TypeName = "UUID")]
    [ForeignKey(nameof(Adopter))]
    public Guid AdopterId { get; set; }
    public User Adopter { get; set; } = null!;

    [Required]
    [Column("type_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetType))]
    public Guid PetTypeId { get; set; }
    public PetType PetType { get; set; } = null!;
}