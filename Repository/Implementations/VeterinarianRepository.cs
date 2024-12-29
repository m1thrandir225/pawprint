using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class VeterinarianRepository  : CrudRepository<Veterinarian>, IVeterinarianRepository
{
    public VeterinarianRepository(ApplicationDbContext context) : base(context)
    {
    }
}