using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain;

[Table("shelters")]
public class Shelter : BaseEntity
{
    [Required]
    [Column("name", TypeName = "VARCHAR(255)")]
    public string Name { get; set; }

    [Required]
    [Column("address", TypeName = "TEXT")]
    public string Address { get; set; }

    [Required]
    [Column("phone_number", TypeName = "TEXT")]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(50)]
    [Column("email")]
    public string Email { get; set; }

    [Column("website", TypeName = "TEXT")]
    public string? Website { get; set; }

    [Column("capacity")]
    public int capacity { get; set; }

    [Column("is_no_kill", TypeName = "BOOLEAN")]
    public bool isNoKill { get; set; } = false;

    [Column("created_at", TypeName = "TIMESTAMPTZ")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    // One-to-many relationship with Pet
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}