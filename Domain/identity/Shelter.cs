using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain;

[Table("shelters")]
public class Shelter : ApplicationUser
{
    [Required]
    [Column("name", TypeName = "VARCHAR(255)")]
    public string Name { get; set; }

    [Required]
    [Column("phone_number", TypeName = "TEXT")]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(50)]
    [Column("email")]
    public string Email { get; set; }

    [Column("website", TypeName = "TEXT")] public string? Website { get; set; }

    [Column("capacity")] public int capacity { get; set; }

    [Column("is_no_kill", TypeName = "BOOLEAN")]
    public bool isNoKill { get; set; } = false;

}