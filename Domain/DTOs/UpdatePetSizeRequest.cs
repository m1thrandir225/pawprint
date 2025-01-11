namespace Domain.DTOs;

public class UpdatePetSizeRequest
{
    public string Name { get; set; }

    public UpdatePetSizeRequest(Guid id, string name)
    {
        Name = name;
    }
}