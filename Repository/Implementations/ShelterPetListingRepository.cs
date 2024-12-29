using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class ShelterPetListingRepository : CrudRepository<ShelterPetListing>, IShelterPetListingRepository
{
    public ShelterPetListingRepository(ApplicationDbContext context) : base(context)
    {
    }
}