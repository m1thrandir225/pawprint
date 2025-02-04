using Domain;
using Domain.enums;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementations;

public class OwnerPetListingRepository : CrudRepository<OwnerPetListing>, IOwnerPetListingRepository
{
    private readonly ApplicationDbContext _context;
    public OwnerPetListingRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public List<OwnerPetListing> FilterListingsByOwner(Guid id)
    {
        var query = _context.OwnerPetListings
            .Include(x => x.Pet.Adoptions)
            .Where(x => x.AdopterId == id);


        return query.ToList();
    }

    public List<OwnerPetListing> FilterListings(Guid? petTypeId, Guid? petSizeId, Guid? petGenderId, string? search)
    {
        var query = _context.OwnerPetListings
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

    public ICollection<OwnerPetListing> FilterByStatus(Guid adoptionStatusId, Guid ownerId)
    {
        var query = _context.OwnerPetListings
            .Include(x => x.Pet)
            .Where(x => x.Pet.AdoptionStatusId == adoptionStatusId).ToList();

        return query;
    }
}