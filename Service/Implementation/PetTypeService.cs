using Domain;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.PetType;
using Repository.Interface;

namespace Service.Implementation;

public class PetTypeService : IPetTypeService
{
    private readonly IPetTypeRepository _repository;

    public PetTypeService(IPetTypeRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<PetType>> GetAllAsync()
    {
        return _repository.GetAll();
    }

    public async Task<PetType> GetByIdAsync(Guid id)
    {
        var petType =  _repository.Get(id);

        return petType;
    }

    public async Task<PetType> CreateAsync(CreatePetTypeRequest dto)
    {
        var petType = new PetType(dto.Name);

        return _repository.Insert(petType);
    }

    public async Task<PetType> UpdateAsync(Guid id, UpdatePetTypeRequest dto)
    {
        var petType = _repository.Get(id);

        if (petType == null)
        {
            return null;
        }

        petType.Name = dto.Name;

        return _repository.Update(petType);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var petType = _repository.Get(id);

        _repository.Delete(petType);
        return Task.FromResult(true);
    }
}