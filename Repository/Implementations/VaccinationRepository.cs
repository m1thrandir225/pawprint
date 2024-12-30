using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class VaccinationRepository : CrudRepository<Vaccination>, IVaccinationRepository
{
    public VaccinationRepository(ApplicationDbContext context) : base(context)
    {
    }
}