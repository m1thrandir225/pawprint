namespace Domain.DTOs.MedicalCondition;

public class UpdateMedicalConditionRequest
{
    public Guid Id { get; set; }
    public Guid MedicalRecordId { get; set; }
    public string ConditionName { get; set; }
    public string? Notes { get; set; }

    public UpdateMedicalConditionRequest(Guid id, Guid medicalRecordId, string conditionName, string? notes)
    {
        Id = id;
        MedicalRecordId = medicalRecordId;
        ConditionName = conditionName;
        Notes = notes;
    }
}