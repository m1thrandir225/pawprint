using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;

namespace Domain;

[Table("shelters")]
public class Shelter : ApplicationUser
{
    [Required]
    [Column("name", TypeName = "TEXT")]
    public string Name { get; set; }

    [Column("website", TypeName = "TEXT")] 
    public string? Website { get; set; }

    [Required]
    [Column("capacity")] 
    public int Capacity { get; set; }

    [Required]
    [Column("is_no_kill", TypeName = "BOOLEAN")]
    public bool isNoKill { get; set; } = false;
}