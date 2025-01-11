using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class VeterinarianSpecializationRepository : CrudRepository<VeterinarianSpecilization>, IVeterinarianSpecializationRepository
{
    public VeterinarianSpecializationRepository(ApplicationDbContext context) : base(context)
    {
    }
}