using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("adoption_statuses")]
public class AdoptionStatus: BaseEntity
{
    [Required]
    public string Name { get; set; }

    public virtual ICollection<Pet> Pets { get; set; }
}