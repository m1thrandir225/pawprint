namespace Domain.DTOs;

public class CreatePetRequest
{
    public string Name { get; set; }

    public string? Breed { get; set; }

    public string? AvatarImg { get; set; }

    public string[]? ImageShowcase { get; set; } 

    public int AgeYears { get; set; }

    public Guid PetTypeId { get; set; }

    public Guid PetGenderId { get; set; }

    public Guid PetSizeId { get; set; }

    public Guid AdoptionStatusId { get; set; }

    public Guid HealthStatusId { get; set; }

    public bool GoodWithChildren { get; set; } = false;
    public bool GoodWithCats { get; set; } = false;
    public bool GoodWithDogs { get; set; } = false;

    public int EnergyLevel { get; set; }

    public string? Description { get; set; }
    public string? SpecialRequirements { get; set; }
    public string? BehaviorialNotes { get; set; }
    public DateTime? IntakeDate { get; set; }

}