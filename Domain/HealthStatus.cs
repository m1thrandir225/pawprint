using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain;

[Table("health_statuses")]
public class HealthStatus : BaseEntity
{
    [Required]
    [Column("name", TypeName = "TEXT")]
    public string Name { get; set; }

    // One-to-many relationship with Pet
    // public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
    public HealthStatus()
    {
        
    }

    public HealthStatus(string name)
    {
        Id = new Guid();
        Name = name;
    }
}
