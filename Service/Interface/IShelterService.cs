
using Domain;
using Domain.DTOs;
using Domain.DTOs.Identity;
using Domain.identity;

namespace Service.Interface
{
    public interface IShelterService : ICRUDService<Shelter, CreateShelterRequest, UpdateShelterRequest>
    {
        public List<MonthlyCreation> YearlyShelterStatistics();

    }
}