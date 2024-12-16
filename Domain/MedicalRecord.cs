using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Domain;

[Table("medical_records")]
public class MedicalRecord : BaseEntity
{
    [Column("vet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Veterinarian))]
    public Guid VetId { get; set; }

    [Column("microchip_number", TypeName = "VARCHAR(255)")]
    public string? MicrochipNumber { get; set; }

    [Column("spay_neuter_status", TypeName = "BOOLEAN")]
    public bool NeuterStatus { get; set; } = false;

    [Column("last_medical_checkup")]
    public DateOnly? LastMedicalCheckup { get; set; }
    [Required]
    public Veterinarian Veterinarian { get; init; }


}