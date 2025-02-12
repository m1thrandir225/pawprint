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
        return await _repository.GetAll();
    }

    public async Task<PetType> GetByIdAsync(Guid id)
    {
        var petType =  await _repository.Get(id);

        return petType;
    }

    public async Task<PetType> CreateAsync(CreatePetTypeRequest dto)
    {
        var petType = new PetType(dto.Name);

        return await _repository.Insert(petType);
    }

    public async Task<PetType> UpdateAsync(Guid id, UpdatePetTypeRequest dto)
    {
        var petType = await _repository.Get(id);

        if (petType == null)
        {
            return null;
        }

        petType.Name = dto.Name;

        return await _repository.Update(petType);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var petType = await _repository.Get(id);

        await _repository.Delete(petType);
        return true;
    }
}