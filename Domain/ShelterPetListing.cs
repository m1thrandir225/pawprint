using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.enums;
using Microsoft.EntityFrameworkCore;

namespace Domain;

[Table("shelter_pet_listings")]
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
    [Column("adoption_fee", TypeName = "DECIMAL")]
    public float AdoptionFee { get; set; }


    [JsonIgnore]
    public virtual ICollection<Adoption> Adoptions { get; set; } = new List<Adoption>();
 
    public ShelterPetListing()
    {
    }
    
    public ShelterPetListing(Guid petId, Guid medicalRecordId, Guid shelterId, DateOnly? intakeDate, float adoptionFee=0)
    {
        Id = Guid.NewGuid();
        PetId = petId;
        MedicalRecordId = medicalRecordId;
        ShelterId = shelterId;
        IntakeDate = intakeDate;
        AdoptionFee = adoptionFee;
    }
}
