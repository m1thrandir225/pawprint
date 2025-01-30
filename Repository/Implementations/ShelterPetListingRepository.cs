using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementations;

public class ShelterPetListingRepository : CrudRepository<ShelterPetListing>, IShelterPetListingRepository
{
    private readonly ApplicationDbContext _context;
    public ShelterPetListingRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public List<ShelterPetListing> GetListingByShelter(Guid shelterId)
    {
        return _context.ShelterPetListings.Where(x => x.ShelterId == shelterId).ToList();
    }
}