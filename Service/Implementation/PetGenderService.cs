using Domain;
using Domain.DTOs;
using Domain.DTOs.PetGender;
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
        return await _repository.GetAll();
    }

    public async Task<PetGender> GetByIdAsync(Guid id)
    {
        return await _repository.Get(id);;
    }

    public async Task<PetGender> CreateAsync(CreatePetGenderRequest dto)
    {
        var petGender = new PetGender(dto.Name);
        
        return await _repository.Insert(petGender);
    }

    public async Task<PetGender> UpdateAsync(Guid id, UpdatePetGenderRequest dto)
    {
        var petGender = await _repository.Get(id);

        if (petGender == null)
        {
            return null;
        }
        
        petGender.Name = dto.Name;
        
        return await _repository.Update(petGender);
    }
    
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var petGender = await _repository.Get(id);
        
        await _repository.Delete(petGender);
        return true;
    }
}