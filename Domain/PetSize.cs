using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("pet_sizes")]
public class PetSize : BaseEntity
{
    [Required]
    public string Name { get; set; }
}