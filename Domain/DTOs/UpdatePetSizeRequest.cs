namespace Domain.DTOs;

public class UpdatePetSizeRequest
{
    public string Name { get; set; }

    public UpdatePetSizeRequest(string name)
    {
        Name = name;
    }
}