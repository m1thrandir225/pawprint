using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IAdoptionService : ICRUDService<Adoption, CreateAdoptionRequest, UpdateAdoptionRequest>
{
    public List<Adoption> GetAdoptionsForPet(Guid id);
}