using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class PetTypeRepository : CrudRepository<PetType>, IPetTypeRepository
{
    public PetTypeRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}