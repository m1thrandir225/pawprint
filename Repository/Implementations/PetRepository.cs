using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class PetRepository : CrudRepository<Pet>, IPetRepository
{
    public PetRepository(ApplicationDbContext context) : base(context)
    {
    }
}