using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("vaccinations")]
public class Vaccination : BaseEntity
{
    // I think this annotation isn't required.
    [ForeignKey(nameof(MedicalRecord))] public Guid MedicalRecordId { get; set; }

    [Required] public string VaccineName { get; set; }

    [Required] public DateOnly VaccineDate { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; }
}