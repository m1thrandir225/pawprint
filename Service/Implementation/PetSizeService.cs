using Domain;
using Domain.DTOs;
using Domain.DTOs.PetSize;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class PetSizeService : IPetSizeService
{
    private readonly IPetSizeRepository _repository;

    public PetSizeService(IPetSizeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PetSize>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<PetSize> GetByIdAsync(Guid id)
    {
        return await _repository.Get(id);
    }

    public async Task<PetSize> CreateAsync(CreatePetSizeRequest dto)
    {
        var petSize = new PetSize(dto.Name);
        
        return await _repository.Insert(petSize);
    }

    public async Task<PetSize> UpdateAsync(Guid id, UpdatePetSizeRequest dto)
    {
        var petSize = await _repository.Get(id);

        if (petSize == null)
        {
            return null;
        }
        
        petSize.Name = dto.Name;
        
        return await _repository.Update(petSize);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var petSize =  await _repository.Get(id);
        
        await _repository.Delete(petSize);
        return true;
    }
}