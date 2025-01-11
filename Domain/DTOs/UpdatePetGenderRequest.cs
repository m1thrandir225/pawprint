namespace Domain.DTOs;

public class UpdatePetGenderRequest
{
    public String Name { get; set; }

    public UpdatePetGenderRequest(Guid id, string name)
    {
        Name = name;
    }
}