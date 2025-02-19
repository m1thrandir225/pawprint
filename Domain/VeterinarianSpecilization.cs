using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("vet_specializations")]
public class VeterinarianSpecilization : BaseEntity
{
    [Required]
    [Column("vet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Veterinarian))]
    public Guid VeterinarianId { get; set; }

    //public virtual Veterinarian Veterinarian { get; init; } = null!;

    [Required]
    [Column("specialization", TypeName = "TEXT")]
    public string Specialization { get; set; } = null!;

    public VeterinarianSpecilization()
    {
        
    }
    public VeterinarianSpecilization(Guid veterinarianId, string specialization)
    {
        Id = Guid.NewGuid(); 
        VeterinarianId = veterinarianId;
        Specialization = specialization; 
    }

}