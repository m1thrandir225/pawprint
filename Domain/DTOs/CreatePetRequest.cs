namespace Domain.DTOs;

/**
 * This is used in CreateShelterPetListing & CreateOwnerPetListing
 */
public class CreatePetRequest
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
}