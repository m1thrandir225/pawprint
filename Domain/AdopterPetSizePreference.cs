using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class AdopterPetSizePreference
{
    [Required]
    [Column("adopter_id", TypeName = "UUID")]
    [ForeignKey(nameof(Adopter))]
    public Guid AdopterId { get; set; }

    [Required]
    public User Adopter { get; set; } = null!;

    [Required]
    [Column("size_id", TypeName = "UUID")]
    [ForeignKey(nameof(PetSize))]
    public Guid PetSizeId { get; set; }

    [Required]
    public PetSize PetSize { get; set; } = null!;
}