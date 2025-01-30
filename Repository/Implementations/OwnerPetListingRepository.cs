using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class OwnerPetListingRepository : CrudRepository<OwnerPetListing>, IOwnerPetListingRepository
{
    private readonly ApplicationDbContext _context;
    public OwnerPetListingRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public List<OwnerPetListing> GetListingsByOwner(Guid id)
    {
        return _context.OwnerPetListings.Where(x => x.AdopterId == id).ToList();
    }
}