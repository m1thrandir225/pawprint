using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("medical_conditions")]
public class MedicalCondition : BaseEntity
{
    [Required]
    [ForeignKey(nameof(MedicalRecord))]
    [Column("medical_record_id", TypeName = "UUID")] 
    public Guid MedicalRecordId { get; set; }
    
    [Required]
    [Column("condition_name", TypeName = "VARCHAR(100")]
    public string ConditionName { get; set; }

    [Column("notes", TypeName = "VARCHAR(255)")]
    public string? Notes { get; set; }

    public Guid MedicalRecordId { get; set; } // Required foreign key property
    public MedicalRecord MedicalRecord { get; init; } = null!;

}