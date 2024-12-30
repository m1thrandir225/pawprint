using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class ShelterRepository : CrudRepository<Shelter>, IShelterRepository
{
    public ShelterRepository(ApplicationDbContext context) : base(context)
    {
    }
}