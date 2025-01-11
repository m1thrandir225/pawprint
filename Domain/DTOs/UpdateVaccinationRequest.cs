namespace Domain.DTOs;

public class UpdateVaccinationRequest
{
    public Guid MedicalRecordId { get; set; }
    public string VaccinationName { get; set; }
    public DateOnly VaccineDate{ get; set; }

    public UpdateVaccinationRequest(Guid medicalRecordId, string vaccineName, DateOnly vaccineDate)
    {
        MedicalRecordId = medicalRecordId;
        VaccinationName = vaccineName;
        VaccineDate = vaccineDate;
    }
}