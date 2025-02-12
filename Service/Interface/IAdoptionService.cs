using Domain;
using Domain.DTOs;
using Domain.DTOs.Adoption;
using Domain.enums;

namespace Service.Interface;

public interface IAdoptionService : ICRUDService<Adoption, CreateAdoptionDTO, UpdateAdoptionRequest>
{
    public List<Adoption> GetAdoptionsForPet(Guid id);

    public Task<List<Adoption>> GetAdoptionsForUser(Guid id);

    public Task<Adoption> UpdateApprovalStatus(Guid id, ApprovalStatus status);

    public List<MonthlyCreation> YearlyAdoptions();
}