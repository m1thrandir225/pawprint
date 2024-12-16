using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("medical_conditions")]
public class MedicalCondition : BaseEntity
{
    [ForeignKey(nameof(MedicalRecord))]
    [Column("medical_record_id", TypeName = "UUID")]
    public Guid MedicalRecordId { get; set; }

    [Column("condition_name", TypeName = "VARCHAR(100")]
    public string ConditionName { get; set; }

    [Column("notes", TypeName = "VARCHAR(255)")]
    public string? Notes { get; set; }

    public MedicalRecord MedicalRecord { get; init; } = null!;

}