namespace Domain.DTOs;

public class UpdateMedicalRecordRequest
{
    public Guid Id { get; set; }
    public Guid VetId { get; set; }
    public bool SpayNeuterStatus { get; set; }
    public DateOnly? LastMedicalCheckup { get; set; }
    public string? MicrochipNumber { get; set; }

    public UpdateMedicalRecordRequest(Guid id, Guid vetId, bool spayNeuterStatus, DateOnly? lastMedicalCheckup, string? microchipNumber)
    {
        Id = id;
        VetId = vetId;
        SpayNeuterStatus = spayNeuterStatus;
        LastMedicalCheckup = lastMedicalCheckup;
        MicrochipNumber = microchipNumber;
    }
}