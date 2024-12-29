using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class PetGenderRepository : CrudRepository<PetGender>, IPetGenderRepository
{
    public PetGenderRepository(ApplicationDbContext context) : base(context)
    {
    }
}