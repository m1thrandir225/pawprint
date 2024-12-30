using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class AdoptionRepository : CrudRepository<Adoption>, IAdoptionRepository
{
    public AdoptionRepository(ApplicationDbContext context) : base(context)
    {
    }
}