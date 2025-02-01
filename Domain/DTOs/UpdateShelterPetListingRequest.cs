using Domain.enums;

namespace Domain.DTOs;

public class UpdateShelterPetListingRequest
{
    public Guid Id { get; set; }
    public DateOnly? IntakeDate { get; set; }
    public ApprovalStatus Approved { get; set; }

    public float AdoptionFee { get; set; }

    public UpdateShelterPetListingRequest(Guid id, DateOnly? intakeDate, ApprovalStatus approved, float adoptionFee)
    {
        Id = id;
        IntakeDate = intakeDate;
        Approved = approved;
        AdoptionFee = adoptionFee;
    }
}