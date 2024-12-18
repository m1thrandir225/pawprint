namespace Domain;

public class Adopter :BaseEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public bool HasChildren { get; set; }
    public bool HasOtherPets { get; set; }
    public string HomeType { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AdopterPetTypePreference> AdopterPetTypePreferences { get; set; }
    public virtual ICollection<Adoption> Adoptions { get; set; }
}