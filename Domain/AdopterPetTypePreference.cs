namespace Domain;

[Table("adopter_pet_type_preferences")]
public class AdopterPetTypePreference
{
    public class AdopterPetTypePreference :
    {
        [Required]
        [Column("adopter_id", TypeName = "UUID")]
        [ForeignKey(nameof(Adopter))]
        public Guid AdopterId { get; set; }
        public Adopter Adopter { get; set; } = null!;

        [Required]
        [Column("type_id", TypeName = "UUID")]
        [ForeignKey(nameof(PetType))]
        public Guid PetTypeId { get; set; }
        public PetType PetType { get; set; } = null!;
    }

}