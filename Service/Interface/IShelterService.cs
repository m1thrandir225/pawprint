
using Domain;
using Domain.DTOs;

namespace Service.Interface
{
    public interface IShelterService : ICRUDService<Shelter, CreateShelterRequest, UpdateShelterRequest>
    {
        public List<MonthlyCreation> YearlyShelterStatistics();

    }
}