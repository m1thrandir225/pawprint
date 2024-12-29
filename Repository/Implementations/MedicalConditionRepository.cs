using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class MedicalConditionRepository : CrudRepository<MedicalCondition>, IMedicalConditionRepository
{
    public MedicalConditionRepository(ApplicationDbContext context) : base(context)
    {
    }
}