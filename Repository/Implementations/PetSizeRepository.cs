using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class PetSizeRepository : CrudRepository<PetSize>, IPetSizeRepository
{
    public PetSizeRepository(ApplicationDbContext context) : base(context)
    {
    }
}