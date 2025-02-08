using Domain.DTOs.MedicalCondition;
using Domain.DTOs.Vaccination;
using Domain.enums;

namespace Domain.DTOs.OwnerPetListing;

public class CreateOwnerPetListingRequest
{
    public string Name { get; set; }
    public string Breed { get; set; }
    //AVATAR_IMG
    //IMAGE_SHOWCASE
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
    public bool spayNeuterStatus  { get; set; }
    public DateOnly? LastMedicalCheckup { get; set; }
    public string MicrochipNumber { get; set; }
    public List<MedicalConditionDTO>? MedicalConditions { get; set; }
    public List<VaccinationDTO> Vaccinations { get; set; }
    public float Fee { get; set; }
    public string FeeCurrency { get; set; }
    public Guid AdopterId { get; set; }
    public Guid PetId { get; set; }
    public Guid SurrenderReasonId { get; set; }
    public DateTime? ReviewDate { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public ApprovalStatus? Approved { get; set; }
    public float AdoptionFee { get; set; }

}