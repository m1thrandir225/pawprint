using Domain;
using Domain.DTOs;

namespace Repository.Interface;

public interface IShelterRepository : ICrudRepository<Shelter>
{
    public List<MonthlyCreation> YearlyShelterStatistics();
}