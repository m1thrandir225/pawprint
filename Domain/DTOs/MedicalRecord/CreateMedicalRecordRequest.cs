namespace Domain.DTOs.MedicalRecord;

public class CreateMedicalRecordRequest
{
    public Guid VetId { get; set; }
    public bool SpayNeuterStatus { get; set; }
    public DateOnly? LastMedicalCheckup { get; set; }
    public string? MicrochipNumber { get; set; }
}