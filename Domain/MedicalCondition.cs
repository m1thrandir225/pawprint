using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("medical_conditions")]
public class MedicalCondition : BaseEntity
{
    [Required]
    [ForeignKey(nameof(MedicalRecord))]
    [Column("medical_record_id", TypeName = "UUID")] 
    public Guid MedicalRecordId { get; set; } // Required foreign key property
    public virtual MedicalRecord MedicalRecord { get; init; } = null!;
    
    [Required]
    [Column("condition_name", TypeName = "TEXT")]
    public string ConditionName { get; set; }

    [Column("notes", TypeName = "TEXT")]
    public string? Notes { get; set; }
    
    
    public MedicalCondition() { }

    public MedicalCondition(Guid medicalRecordId, string conditionName, string? notes)
    {
        Id = Guid.NewGuid();
        MedicalRecordId = medicalRecordId;
        ConditionName = conditionName;
        Notes = notes;
    }

}