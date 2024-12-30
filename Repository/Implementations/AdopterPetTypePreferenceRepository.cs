using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class AdopterPetTypePreferenceRepository : CrudRepository<AdopterPetTypePreference>, IAdopterPetTypePreferenceRepository
{
    public AdopterPetTypePreferenceRepository(ApplicationDbContext context) : base(context)
    {
    }
}