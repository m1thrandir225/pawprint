using System.Globalization;
using CsvHelper;
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

    public async Task<byte[]> GetAllCsv()
    {
        var pets = await _repository.GetAll();
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream, leaveOpen: true))
        using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        {
            // Write header
            csvWriter.WriteField("Name");
            csvWriter.WriteField("Breed");
            csvWriter.WriteField("AvatarImg");
            csvWriter.WriteField("ImageShowcase");
            csvWriter.WriteField("AgeYears");
            csvWriter.WriteField("PetType");
            csvWriter.WriteField("PetGender");
            csvWriter.WriteField("PetSize");
            csvWriter.WriteField("AdoptionStatus");
            csvWriter.WriteField("HealthStatus");
            csvWriter.WriteField("GoodWithChildren");
            csvWriter.WriteField("GoodWithCats");
            csvWriter.WriteField("GoodWithDogs");
            csvWriter.WriteField("EnergyLevel");
            csvWriter.WriteField("Description");
            csvWriter.WriteField("SpecialRequirements");
            csvWriter.WriteField("BehaviorialNotes");
            csvWriter.WriteField("IntakeDate");
            csvWriter.NextRecord();

            foreach (var pet in pets)
            {
                csvWriter.WriteField(pet.Name);
                csvWriter.WriteField(pet.Breed);
                // csvWriter.WriteField(pet.AvatarImg);
                // csvWriter.WriteField(string.Join(";", pet.ImageShowcase));
                csvWriter.WriteField(pet.AgeYears);
                csvWriter.WriteField(pet.PetType?.Name);
                csvWriter.WriteField(pet.PetGender?.Name);
                csvWriter.WriteField(pet.PetSize?.Name);
                csvWriter.WriteField(pet.AdoptionStatus?.Name);
                csvWriter.WriteField(pet.HealthStatus?.Name);
                csvWriter.WriteField(pet.GoodWithChildren);
                csvWriter.WriteField(pet.GoodWithCats);
                csvWriter.WriteField(pet.GoodWithDogs);
                csvWriter.WriteField(pet.EnergyLevel);
                csvWriter.WriteField(pet.Description);
                csvWriter.WriteField(pet.SpecialRequirements);
                csvWriter.WriteField(pet.BehaviorialNotes);
                csvWriter.WriteField(pet.IntakeDate?.ToString("yyyy-MM-dd"));
                csvWriter.NextRecord();
            }
        }

        return memoryStream.ToArray();
    }
}