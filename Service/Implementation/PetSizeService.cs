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
        return _repository.GetAll();
    }

    public async Task<PetSize> GetByIdAsync(Guid id)
    {
        return _repository.Get(id);
    }

    public async Task<PetSize> CreateAsync(CreatePetSizeRequest dto)
    {
        var petSize = new PetSize(dto.Name);
        
        return _repository.Insert(petSize);
    }

    public async Task<PetSize> UpdateAsync(Guid id, UpdatePetSizeRequest dto)
    {
        var petSize = _repository.Get(id);

        if (petSize == null)
        {
            return null;
        }
        
        petSize.Name = dto.Name;
        
        return _repository.Update(petSize);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var petSize = _repository.Get(id);
        
        _repository.Delete(petSize);
        return Task.FromResult(true); 
    }
}