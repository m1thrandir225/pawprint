using Domain;
using Domain.DTOs;
using Domain.DTOs.Pet;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class PetService : IPetService
{
    private readonly IPetRepository _repository;

    public PetService(IPetRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Pet>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<Pet> GetByIdAsync(Guid id)
    {
        return await _repository.Get(id);
    }

    public async Task<Pet> CreateAsync(CreatePetDTO dto)
    {
        var pet = new Pet(
            dto.Name,
            dto.AvatarImg,
            dto.PetTypeId,
            dto.PetGenderId,
            dto.PetSizeId,
            dto.AdoptionStatusId,
            dto.HealthStatusId,
            dto.AgeYears,
            dto.EnergyLevel,
            dto.GoodWithChildren,
            dto.GoodWithCats,
            dto.GoodWithDogs,
            dto.Breed,
            null,
            dto.SpecialRequirements,
            dto.BehaviorialNotes,
            dto.IntakeDate,
            dto.ImageShowcase);

        return await _repository.Insert(pet);
    }

    public async Task<Pet> UpdateAsync(Guid id, UpdatePetDTO dto)
    {
        var pet = await _repository.Get(id);

        if (pet == null)
        {
            return null;
        }

        pet.Name = dto.Name;
        pet.AvatarImg = dto.AvatarImg;
        pet.PetTypeId = dto.PetTypeId;
        pet.PetGenderId = dto.PetGenderId;
        pet.PetSizeId = dto.PetSizeId;
        pet.AdoptionStatusId = dto.AdoptionStatusId;
        pet.HealthStatusId = dto.HealthStatusId;
        pet.AgeYears = dto.AgeYears;
        pet.EnergyLevel = dto.EnergyLevel;
        pet.GoodWithChildren = dto.GoodWithChildren;
        pet.GoodWithCats = dto.GoodWithCats;
        pet.GoodWithDogs = dto.GoodWithDogs;
        pet.Breed = dto.Breed;
        pet.Description = dto.Description;
        pet.SpecialRequirements = dto.SpecialRequirements;
        pet.BehaviorialNotes = dto.BehaviorialNotes;
        pet.IntakeDate = dto.IntakeDate;
        pet.ImageShowcase = dto.ImageShowcase;
        
        return await _repository.Update(pet);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var pet = await _repository.Get(id);
        
        await _repository.Delete(pet);
        return true;
    }
}