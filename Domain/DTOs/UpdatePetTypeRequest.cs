namespace Domain.DTOs;

public class UpdatePetTypeRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public UpdatePetTypeRequest(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}