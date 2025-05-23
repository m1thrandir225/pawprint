using Domain;
using Domain.DTOs;

namespace Repository.Interface;

public interface IAdoptionRepository: ICrudRepository<Adoption>
{
    public List<Adoption> GetAdoptionsForPet(Guid id);

    public Task<List<Adoption>> GetAdoptionsForUser(Guid id);

    public List<MonthlyCreation> YearlyAdoptions();
}