using Domain;
using Repository.Interface;

namespace Repository.Implementations;

public class MedicalRecordRepository: CrudRepository<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(ApplicationDbContext context) : base(context)
    {
    }
}