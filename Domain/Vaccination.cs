using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("vaccinations")]
public class Vaccination : BaseEntity
{
    [Column("medical_record_id", TypeName = "UUID")]
    [ForeignKey(nameof(MedicalRecord))]
    public Guid MedicalRecordId { get; set; }

    [Required]
    [Column("vaccine_name", TypeName = "TEXT")]
    public string VaccineName { get; set; }

    [Required]
    [Column("vaccine_date", TypeName = "Date")]
    public DateOnly VaccineDate { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; }

    public Vaccination()
    {
        
    }

    public Vaccination(Guid medicalRecordId, string vaccineName, DateOnly vaccineDate)
    {
        Id = Guid.NewGuid();
        MedicalRecordId = medicalRecordId;
        VaccineName = vaccineName;
        VaccineDate = vaccineDate;
    }
}