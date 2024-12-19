using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("health_statuses")]
public class HealthStatus : BaseEntity
{
    [Required]
    public string Name { get; set; }
}