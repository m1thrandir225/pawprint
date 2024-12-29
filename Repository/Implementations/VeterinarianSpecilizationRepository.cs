using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class VeterinarianSpecilizationRepository : CrudRepository<VeterinarianSpecilization>, IVeterinarianSpecilizationRepository
{
    public VeterinarianSpecilizationRepository(ApplicationDbContext context) : base(context)
    {
    }
}