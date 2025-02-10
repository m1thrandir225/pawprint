using Domain;
using Domain.DTOs;
using Domain.identity;

namespace Repository.Interface;

public interface IShelterRepository : ICrudRepository<Shelter>
{
    public List<MonthlyCreation> YearlyShelterStatistics();
}