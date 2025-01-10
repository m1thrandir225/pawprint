using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Domain;

/*
 * Email, Phone Number & Roles are inherited from IdentityUser
 */
[Table("users")]
public class User : ApplicationUser
{
    [Required]
    [Column("first_name", TypeName = "varchar(11)")]
    public string FirstName { get; set; }

    [Required]
    [Column("last_name", TypeName = "varchar(11)")]
    public string LastName { get; set; }

    [Column("has_children", TypeName = "BOOLEAN")]
    public bool HasChildren { get; set; } = false;

    [Column("has_other_pets", TypeName = "BOOLEAN")]
    public bool HasOtherPets { get; set; } = false;

    [Column("home_type", TypeName = "VARCHAR(255)")]
    public string HomeType { get; set; }
}