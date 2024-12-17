using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class CustomUser : IdentityUser
{
    [Required]
    [Column("first_name", TypeName = "varchar(11)")]
    public string FirstName { get; set; }

    [Required]
    [Column("last_name", TypeName = "varchar(11)")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [Column("email", TypeName = "VARCHAR(100)")]
    public string Email { get; set; }

    [Required]
    [Column("phone_number", TypeName = "varchar(11)")]
    public string PhoneNumber { get; set; }

    [Column("has_children", TypeName = "BOOLEAN")]
    public bool HasChildren { get; set; } = false;

    [Column("has_other_pets", TypeName = "BOOLEAN")]
    public bool HasOtherPets { get; set; } = false;

    [Column("home_type", TypeName = "VARCHAR(255)")]
    public string HomeType { get; set; }


    [Column("created_at", TypeName = "TIMESTAMPTZ")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

}