using Domain.DTOs.MedicalCondition;
using Domain.DTOs.MedicalRecord;
using Domain.DTOs.Pet;
using Domain.DTOs.Vaccination;
using Domain.DTOs.Veterinarian;

namespace Domain.DTOs.ShelterPetListing;

public class CreateShelterPetListingRequest
{
    public CreatePetRequest Pet { get; set; }
    public CreateVeterinarianForListingRequest Veterinarian { get; set; }
    public CreateMedicalRecordForListingRequest MedicalRecord { get; set; }
    public CreateFeeRequest Fee { get; set; }

}
/**
form: {
    'pet': {} -> JSON FIELDS,
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