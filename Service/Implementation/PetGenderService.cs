using Domain;
using Domain.DTOs;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class PetGenderService : IPetGenderService
{
    private readonly IPetGenderRepository _repository;

    public PetGenderService(IPetGenderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PetGender>> GetAllAsync()
    {
        return _repository.GetAll();
    }

    public async Task<PetGender> GetByIdAsync(Guid id)
    {
        return _repository.Get(id);;
    }

    public async Task<PetGender> CreateAsync(CreatePetGenderRequest dto)
    {
        var petGender = new PetGender(dto.Name);
        
        return _repository.Insert(petGender);
    }

    public async Task<PetGender> UpdateAsync(Guid id, UpdatePetGenderRequest dto)
    {
        var petGender = _repository.Get(id);

        if (petGender == null)
        {
            return null;
        }
        
        petGender.Name = dto.Name;
        
        return _repository.Update(petGender);
    }
    
    
    public Task<bool> DeleteAsync(Guid id)
    {
        var petGender = _repository.Get(id);
        
        _repository.Delete(petGender);
        return Task.FromResult(true);
    }
}