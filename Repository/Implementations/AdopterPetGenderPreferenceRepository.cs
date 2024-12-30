using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class AdopterPetGenderPreferenceRepository : CrudRepository<AdopterPetGenderPreference>, IAdopterPetGenderPreferenceRepository
{
    public AdopterPetGenderPreferenceRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}