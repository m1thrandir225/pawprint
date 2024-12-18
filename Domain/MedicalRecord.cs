using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Domain;

[Table("medical_records")]
public class MedicalRecord : BaseEntity
{
    // [Column("vet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Veterinarian))] public Guid VetId { get; set; }
    
    public bool SpayNeuterStatus { get; set; }

    public DateOnly? LastMedicalCheckup { get; set; }
    
    public string? MicrochipNumber { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    // [Required] public Veterinarian Veterinarian { get; init; }

    // Don't know if Required is needed.
    [Required] public virtual Veterinarian Veterinarian { get; set; }
    public virtual ICollection<MedicalCondition> MedicalConditions { get; set; }
    public virtual ICollection<Vaccination> Vaccinations { get; set; }
}