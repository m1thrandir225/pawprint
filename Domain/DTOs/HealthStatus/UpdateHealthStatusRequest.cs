namespace Domain.DTOs.HealthStatus;

public class UpdateHealthStatusRequest
{
    public string Name { get; set; }

    public UpdateHealthStatusRequest(string name)
    {
        Name = name;
    }
}