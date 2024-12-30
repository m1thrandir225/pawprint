using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class HealthStatusRepository : CrudRepository<HealthStatus>, IHealthStatusRepository
{
    public HealthStatusRepository(ApplicationDbContext context) : base(context)
    {
    }
}