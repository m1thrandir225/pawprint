namespace Domain.DTOs.PetGender;

public class UpdatePetGenderRequest
{
    public String Name { get; set; }

    public UpdatePetGenderRequest(string name)
    {
        Name = name;
    }
}