using Domain;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;

namespace Repository.Implementations;

public class ShelterPetListingRepository : CrudRepository<ShelterPetListing>, IShelterPetListingRepository
{
    private readonly ApplicationDbContext _context;

    private DbSet<ShelterPetListing> _entities;

    public ShelterPetListingRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _entities = context.Set<ShelterPetListing>();
    }

    public IEnumerable<ShelterPetListing> GetAllWithJoins()
    {
        return _entities.Include(x => x.Pet)
        .Include(x => x.Shelter)
        .Include(x => x.MedicalRecord)
        .ToList();
    }
}