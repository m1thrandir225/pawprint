using Domain;
using Domain.DTOs;
using Domain.DTOs.Adoption;

namespace Service.Interface;

public interface IAdoptionService : ICRUDService<Adoption, CreateAdoptionDTO, UpdateAdoptionRequest>
{
    public List<Adoption> GetAdoptionsForPet(Guid id);

    public List<MonthlyCreation> YearlyAdoptions();
}