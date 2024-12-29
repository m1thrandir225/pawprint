using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class OwnerPetListingRepository : CrudRepository<OwnerPetListing>, IOwnerPetListingRepository
{
    public OwnerPetListingRepository(ApplicationDbContext context) : base(context)
    {
    }
}