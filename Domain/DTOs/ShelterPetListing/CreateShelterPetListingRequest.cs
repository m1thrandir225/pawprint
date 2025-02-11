using Domain.DTOs.MedicalCondition;
using Domain.DTOs.MedicalRecord;
using Domain.DTOs.Pet;
using Domain.DTOs.Vaccination;
using Domain.DTOs.Veterinarian;

namespace Domain.DTOs.ShelterPetListing;

public class CreateShelterPetListingRequest
{
    public string Name { get; set; }
    public string Breed { get; set; }
    public int AgeYears { get; set; }
    public Guid PetTypeId { get; set; }
    public Guid PetGenderId { get; set; }
    public Guid PetSizeId { get; set; }
    public Guid HealthStatusId { get; set; }
    public bool GoodWithChildren { get; set; }
    public bool GoodWithDogs { get; set; }
    public bool GoodWithCats { get; set; }
    public int EnergyLevel { get; set; }
    public string? SpecialRequirements { get; set; }
    public string? BehaviorialNotes { get; set; }
    public string VeterinarianName { get; set; }
    public string VeterinarianContactNumber { get; set; }
    public string VeterinarianClinicName { get; set; }
    public string VeterinarianEmail { get; set; }
    public List<string> VeterinarianSpecializations { get; set; }
    public bool SpayNeuterStatus  { get; set; }
    public DateOnly? LastMedicalCheckup { get; set; }
    public string? MicrochipNumber { get; set; }
    public List<MedicalConditionDTO>? MedicalConditions { get; set; }
    public List<VaccinationDTO> Vaccinations { get; set; }
    public float Fee { get; set; }
    public string FeeCurrency { get; set; }
    public DateOnly? IntakeDate { get; set; }
}
/**
form: {
    'pet': {} -> JSON FIELDS,
    'petName': '',
    'veterinarian': {} -> JSON FIELDS
    'medicalRecord': {} -> JSON FIELDS ,
    'fee': {} -> JSON FIELDS,
    'intakeDate': string',
    'imageShowcase': array of images that will be like this:
    'imageShowcase1',
    'imageShowcase2',
    'imageShowcase3....',
    'avatarImg': avatar image as a seperate image
}
*/