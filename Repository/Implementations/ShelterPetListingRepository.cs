using Domain;
using Domain.enums;
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

    public ICollection<ShelterPetListing> GetListingByShelter(Guid shelterId)
    {
        return _context.ShelterPetListings.Where(x => x.ShelterId == shelterId).ToList();
    }

    public ICollection<ShelterPetListing> FilterListings(Guid? petTypeId, Guid? petSizeId, Guid? petGenderId, string? search)
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

    public ICollection<ShelterPetListing> FilterByStatus(ApprovalStatus status, Guid shelterId)
    {
        var listings = _context.ShelterPetListings
        .Where(l => status == ApprovalStatus.APPROVED 
            ? l.Adoptions.Any(a => a.Approved == ApprovalStatus.APPROVED)
            : status  == ApprovalStatus.REJECTED
                ? l.Adoptions.All(a => a.Approved == ApprovalStatus.REJECTED)
                : !l.Adoptions.Any(a => a.Approved == ApprovalStatus.REJECTED || a.Approved == ApprovalStatus.REJECTED) 
        ).Where(l => l.ShelterId == shelterId).ToList();

        return listings;
    }
}