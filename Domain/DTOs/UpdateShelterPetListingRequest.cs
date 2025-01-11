using Domain.enums;

namespace Domain.DTOs;

public class UpdateShelterPetListingRequest
{
    public Guid Id { get; set; }
    public DateOnly? IntakeDate { get; set; }
    public ApprovalStatus Approved { get; set; }

    public UpdateShelterPetListingRequest(Guid id, DateOnly? intakeDate, ApprovalStatus approved)
    {
        Id = id;
        IntakeDate = intakeDate;
        Approved = approved;
    }
}