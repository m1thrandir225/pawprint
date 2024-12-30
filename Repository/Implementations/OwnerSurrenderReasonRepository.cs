using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class OwnerSurrenderReasonRepository : CrudRepository<OwnerSurrenderReason>, IOwnerSurrenderReasonRepository
{
    public OwnerSurrenderReasonRepository(ApplicationDbContext context) : base(context)
    {
    }
}