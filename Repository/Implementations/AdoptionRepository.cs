using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class AdoptionRepository : CrudRepository<Adoption>, IAdoptionRepository
{
    private readonly ApplicationDbContext _context;
    public AdoptionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Adoption> GetAdoptionsForPet(Guid id)
    {
        return _context.Adoptions.Where(a => a.PetId == id).ToList();
    }
}