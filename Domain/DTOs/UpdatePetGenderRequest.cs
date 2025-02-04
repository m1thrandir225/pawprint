namespace Domain.DTOs;

public class UpdatePetGenderRequest
{
    public String Name { get; set; }

    public UpdatePetGenderRequest(string name)
    {
        Name = name;
    }
}