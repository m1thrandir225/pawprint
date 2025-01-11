using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Domain;

[Table("medical_records")]
public class MedicalRecord : BaseEntity
{
    // [Column("vet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Veterinarian))]
    [Column("veterinarian_id", TypeName = "UUID")]
    public Guid VetId { get; set; }

    [Column("spay_neuter_status", TypeName = "BOOLEAN")]
    public bool SpayNeuterStatus { get; set; }

    [Column("last_medical_checkup", TypeName = "DATE")]
    public DateOnly? LastMedicalCheckup { get; set; }

    [Column("microchip_number", TypeName = "TEXT")]
    public string? MicrochipNumber { get; set; }

    [Required]
    [Column("created_at", TypeName = "TIMESTAMPTZ")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    public virtual Veterinarian Veterinarian { get; set; }

    public virtual ICollection<MedicalCondition> MedicalConditions { get; set; }
    public virtual ICollection<Vaccination> Vaccinations { get; set; }
    
    public MedicalRecord()
    {
        MedicalConditions = new HashSet<MedicalCondition>();
        Vaccinations = new HashSet<Vaccination>();
    }
    
    public MedicalRecord(Guid vetId, bool spayNeuterStatus, DateOnly? lastMedicalCheckup, string? microchipNumber)
    {
        Id = Guid.NewGuid();
        VetId = vetId;
        SpayNeuterStatus = spayNeuterStatus;
        LastMedicalCheckup = lastMedicalCheckup;
        MicrochipNumber = microchipNumber;
        CreatedAt = DateTime.Now;
        MedicalConditions = new HashSet<MedicalCondition>();
        Vaccinations = new HashSet<Vaccination>();
    }
}