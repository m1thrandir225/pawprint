namespace Domain.DTOs;

public class UpdateHealthStatusRequest
{
    public string Name { get; set; }

    public UpdateHealthStatusRequest(Guid id, string name)
    {
        Name = name;
    }
}