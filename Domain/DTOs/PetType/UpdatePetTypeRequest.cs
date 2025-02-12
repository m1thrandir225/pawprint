namespace Domain.DTOs.PetType;

public class UpdatePetTypeRequest
{
    public string Name { get; set; }

    public UpdatePetTypeRequest(string name)
    {
        Name = name;
    }
}