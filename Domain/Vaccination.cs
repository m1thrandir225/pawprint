using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("vaccinations")]
public class Vaccination : BaseEntity
{
    [ForeignKey(nameof(MedicalRecord))]
    [Column("medical_record_id", TypeName = "UUID")]
    public Guid MedicalRecordId { get; set; }

    [Required]
    [Column("vaccine_name", TypeName = "VARCHAR(100)")]
    public string VaccineName { get; set; }

    [Required]
    [Column("vaccination_date", TypeName = "date")]
    public DateOnly VaccineDate { get; set; }

    [Required]
    public MedicalRecord MedicalRecord { get; set; } = null!;

}