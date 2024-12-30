using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class AdopterPetSizePreferenceRepository : CrudRepository<AdopterPetSizePreference>, IAdopterPetSizePreferenceRepository
{
    public AdopterPetSizePreferenceRepository(ApplicationDbContext context) : base(context)
    {
        
    }
    
}