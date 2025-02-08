using Domain;
using Domain.DTOs;
using Domain.DTOs.VeterinarianSpecialization;
using Service.Interface;
using Repository.Interface;

namespace Service.Implementation;

public class VeterinarianSpecializationService : IVeterinarianSpecializationService
{
    private readonly IVeterinarianSpecializationRepository _repository;

    public VeterinarianSpecializationService(IVeterinarianSpecializationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<VeterinarianSpecilization>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<VeterinarianSpecilization> GetByIdAsync(Guid id)
    {
        var specialization = await _repository.Get(id);
        return specialization;
    }

    public async Task<VeterinarianSpecilization> CreateAsync(CreateVeterinarianSpecializationRequest dto)
    {
        var specialization = new VeterinarianSpecilization(dto.VeterinarianId, dto.Specialization);
        return await _repository.Insert(specialization);
    }

    public async Task<VeterinarianSpecilization> UpdateAsync(Guid id, UpdateVeterinarianSpecializationRequest dto)
    {
        var specialization = await _repository.Get(id);

        if (specialization == null)
        {
            return null;
        }

        specialization.Specialization = dto.Specialization;

        return await _repository.Update(specialization);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var specialization = await _repository.Get(id);

        if (specialization == null)
        {
            return false;
        }

        await _repository.Delete(specialization);
        return true;
    }
}