using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("pet_genders")]
public class PetGender : BaseEntity
{
    [Required]
    public string Name { get; set; }
    
    public virtual ICollection<Pet> Pets { get; set; }
    
}