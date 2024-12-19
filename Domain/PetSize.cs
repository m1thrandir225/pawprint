using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain;

[Table("pet_sizes")]
public class PetSize : BaseEntity
{
    [Required]
    [Column("name", TypeName = "VARCHAR(255)")]
    public string Name { get; set; }

    // One-to-many relationship with Pet and AdopterPetSizesPreference
    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
    public virtual ICollection<AdopterPetSizePreference> AdopterPetSizesPreference { get; set; } = new List<AdopterPetSizePreference>();
}