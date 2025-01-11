using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("adoption_statuses")]
public class AdoptionStatus: BaseEntity
{
    [Required]
    [Column("name", TypeName = "TEXT")]
    public string Name { get; set; }

    // public virtual ICollection<Pet> Pets { get; set; }
    public AdoptionStatus()
    {
    }
    
    public AdoptionStatus(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}