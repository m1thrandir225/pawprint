using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class AdoptionStatusRepository : CrudRepository<AdoptionStatus>, IAdoptionStatusRepository
{
    public AdoptionStatusRepository(ApplicationDbContext context) : base(context)
    {
    }
}