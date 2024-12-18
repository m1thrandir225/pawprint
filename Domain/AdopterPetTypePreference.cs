namespace Domain;

public class AdopterPetTypePreference
{
    public class AdopterPetTypePreference : BaseEntity
    {
        [Required]
        [Column("adopter_id", TypeName = "UUID")]
        [ForeignKey(nameof(Adopter))]
        public Guid AdopterId { get; set; }
        public Adopter Adopter { get; set; } = null!;

        [Required]
        [Column("pet_size_id", TypeName = "UUID")]
        [ForeignKey(nameof(PetSize))]
        public Guid PetSizeId { get; set; }
        public PetSize PetSize { get; set; } = null!;
    }

}