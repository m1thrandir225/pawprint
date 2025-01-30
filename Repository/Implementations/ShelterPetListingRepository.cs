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

    public List<ShelterPetListing> FilterListings(Guid? petTypeId, Guid? petSizeId, Guid? petGenderId, string? search)
    {
        var query = _context.ShelterPetListings
            .Include(x => x.Pet)
            .AsQueryable();

        if (petTypeId.HasValue)
        {
            query = query.Where(x => x.Pet.PetTypeId == petTypeId.Value);
        }

        if (petSizeId.HasValue)
        {
            query = query.Where(x => x.Pet.PetSizeId == petSizeId.Value);
        }

        if (petGenderId.HasValue)
        {
            query = query.Where(x => x.Pet.PetGenderId == petGenderId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(x => x.Pet.Name.Contains(search));
        }

        return query.ToList();

    }
}