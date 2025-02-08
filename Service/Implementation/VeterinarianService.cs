using Domain;
using Domain.DTOs;
using Domain.DTOs.Veterinarian;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class VeterinarianService : IVeterinarianService
{
    private readonly IVeterinarianRepository _repository;

    public VeterinarianService(IVeterinarianRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Veterinarian>> GetAllAsync()
    {
        return _repository.GetAll();
    }

    public async Task<Veterinarian> GetByIdAsync(Guid id)
    {
        return _repository.Get(id);
    }

    public async Task<Veterinarian> CreateAsync(CreateVeterinarianRequest dto)
    {
        var veterinarian = new Veterinarian
        {
            Name = dto.Name,
            ClinicName = dto.ClinicName,
            ContactNumber = dto.ContactNumber,
            Email = dto.Email,
        };
        
        return _repository.Insert(veterinarian);
    }

    public async Task<Veterinarian> UpdateAsync(Guid id, UpdateVeterinarianRequest dto)
    {
        var veterinarian = _repository.Get(id);

        if (veterinarian == null)
        {
            return null;
        }
        
        veterinarian.Name = dto.Name;
        veterinarian.ContactNumber = dto.ContactNumber;
        veterinarian.Email = dto.Email;
        
        return _repository.Update(veterinarian);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var veterinarian = _repository.Get(id);
        
        _repository.Delete(veterinarian);
        return Task.FromResult(true);
    }
}