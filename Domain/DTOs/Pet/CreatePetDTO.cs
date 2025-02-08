namespace Domain.DTOs.Pet;

public class CreatePetDTO
{
    public string Name { get; set; }
    public string Breed { get; set; }
    public int AgeYears { get; set; }
    public Guid AdoptionStatusId { get; set; }
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
    public string AvatarImg { get; set; }
    public string[] ImageShowcase { get; set; }
    public DateTime? IntakeDate { get; set; }
}