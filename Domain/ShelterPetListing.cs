using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.enums;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class ShelterPetListing : BaseEntity
{
    [Column("pet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Pet))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Guid PetId { get; set; }

    public Pet Pet { get; set; } = null!;

    [Column("medical_record_id", TypeName = "UUID")]
    [ForeignKey(nameof(MedicalRecord))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Guid MedicalRecordId { get; set; }

    public MedicalRecord MedicalRecord { get; set; } = null!;

    [Column("shelter_id", TypeName = "UUID")]
    [ForeignKey(nameof(Shelter))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Guid ShelterId { get; set; }

    [Required]
    public Shelter Shelter { get; set; } = null!;

    [Column("intake_date", TypeName = "DATE")]
    public DateOnly? IntakeDate { get; set; }

    [Required]
    [Column("approved", TypeName = "INTEGER")]
    public ApprovalStatus Approved { get; set; } = ApprovalStatus.PENDING;
}