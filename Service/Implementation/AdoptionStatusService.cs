using Domain.DTOs.AdoptionStatus;

namespace Service.Implementation;

using Domain;
using Service.Interface;
using Domain.DTOs;
using Repository.Interface;

public class AdoptionStatusService : IAdoptionStatusService
{
    private readonly IAdoptionStatusRepository _repository;

    public AdoptionStatusService(IAdoptionStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AdoptionStatus>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<AdoptionStatus> GetByIdAsync(Guid id)
    {
        var adoptionStatus = await _repository.Get(id);
        return adoptionStatus;
    }

    public async Task<AdoptionStatus> CreateAsync(CreateAdoptionStatusRequest dto)
    {
        var adoptionStatus = new AdoptionStatus(dto.Name);
        return await _repository.Insert(adoptionStatus);
    }

    public async Task<AdoptionStatus> UpdateAsync(Guid id, UpdateAdoptionStatusRequest dto)
    {
        var adoptionStatus = await _repository.Get(id);
        if (adoptionStatus == null)
        {
            return null;
        }

        adoptionStatus.Name = dto.Name;

        return await _repository.Update(adoptionStatus);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var adoptionStatus = await _repository.Get(id);
await        _repository.Delete(adoptionStatus);
        return true;
    }
}