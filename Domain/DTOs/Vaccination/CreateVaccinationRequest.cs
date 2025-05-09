namespace Domain.DTOs.Vaccination;

public class CreateVaccinationRequest
{
    public Guid MedicalRecordId { get; set; }
    public string VaccinationName { get; set; }
    public DateOnly VaccineDate{ get; set; }
}