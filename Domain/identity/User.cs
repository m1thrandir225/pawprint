using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.identity;

/*
 * Email, Phone Number & Roles are inherited from IdentityUser
 */
[Table("users")]
public class User : ApplicationUser
{
    [Required]
    [Column("first_name", TypeName = "TEXT")]
    public string FirstName { get; set; }

    [Required]
    [Column("last_name", TypeName = "TEXT")]
    public string LastName { get; set; }

    [Column("has_children", TypeName = "BOOLEAN")]
    public bool HasChildren { get; set; } = false;

    [Column("has_other_pets", TypeName = "BOOLEAN")]
    public bool HasOtherPets { get; set; } = false;

    [Column("home_type", TypeName = "TEXT")]
    public string HomeType { get; set; }

    public virtual ICollection<AdopterPetGenderPreference> AdopterPetGenderPreferences { get; set; } = null!;
    public virtual ICollection<AdopterPetSizePreference> AdopterPetSizePreferences { get; set; } = null!;
    public virtual ICollection<AdopterPetTypePreference> AdopterPetTypePreferences { get; set; } = null!;
}