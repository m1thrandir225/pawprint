namespace Domain.DTOs;

public class UpdateAdoptionStatusRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public UpdateAdoptionStatusRequest(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}