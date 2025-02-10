using Domain.DTOs.MedicalCondition;
using Domain.DTOs.Vaccination;

namespace Domain.DTOs.MedicalRecord;

public class CreateMedicalRecordForListingRequest
{
    public bool SpayNeuterStatus  { get; set; }
    public DateOnly? LastMedicalCheckup { get; set; }
    public string MicrochipNumber { get; set; }
    public List<MedicalConditionDTO>? MedicalConditions { get; set; }
    public List<VaccinationDTO> Vaccinations { get; set; }
}