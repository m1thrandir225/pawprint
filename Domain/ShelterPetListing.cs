using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.enums;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class ShelterPetListing : BaseEntity
{
    [Column("pet_id", TypeName = "UUID")]
    public Guid PetId { get; set; }

    public virtual Pet Pet { get; set; } = null!;

    [Column("medical_record_id", TypeName = "UUID")]
    public Guid MedicalRecordId { get; set; }

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    [Column("shelter_id", TypeName = "UUID")]
    public Guid ShelterId { get; set; }

    [Required]
    public virtual Shelter Shelter { get; set; } = null!;

    [Column("intake_date", TypeName = "DATE")]
    public DateOnly? IntakeDate { get; set; }

    [Required]
    [Column("approved", TypeName = "INTEGER")]
    public virtual ApprovalStatus Approved { get; set; } = ApprovalStatus.PENDING;
    
    public ShelterPetListing()
    {
    }
    
    public ShelterPetListing(Guid petId, Guid medicalRecordId, Guid shelterId, DateOnly? intakeDate)
    {
        Id = Guid.NewGuid();
        PetId = petId;
        MedicalRecordId = medicalRecordId;
        ShelterId = shelterId;
        IntakeDate = intakeDate;
        Approved = ApprovalStatus.PENDING;
    }
}
