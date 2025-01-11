namespace Domain.DTOs;

public class UpdateOwnerSurrenderReasonRequest
{
    public Guid Id { get; set; }
    public string Description { get; set; }

    public UpdateOwnerSurrenderReasonRequest(Guid id, string description)
    {
        Id = id;
        Description = description;
    }
}