using Domain.DTOs.OwnerSurrenderReason;

namespace Service.Implementation;

using Domain;
using Service.Interface;
using Domain.DTOs;
using Repository.Interface;

public class OwnerSurrenderReasonService : IOwnerSurrenderReasonService
{
    private readonly IOwnerSurrenderReasonRepository _repository;

    public OwnerSurrenderReasonService(IOwnerSurrenderReasonRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OwnerSurrenderReason>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<OwnerSurrenderReason> GetByIdAsync(Guid id)
    {
        var ownerSurrenderReason =  await _repository.Get(id);
        return ownerSurrenderReason;
    }

    public async Task<OwnerSurrenderReason> CreateAsync(CreateOwnerSurrenderReasonRequest dto)
    {
        var ownerSurrenderReason = new OwnerSurrenderReason(dto.Description);
        return await _repository.Insert(ownerSurrenderReason);
    }

    public async Task<OwnerSurrenderReason> UpdateAsync(Guid id, UpdateOwnerSurrenderReasonRequest dto)
    {
        var ownerSurrenderReason = await _repository.Get(id);
        if (ownerSurrenderReason == null)
        {
            return null;
        }

        ownerSurrenderReason.Description = dto.Description;

        return await _repository.Update(ownerSurrenderReason);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var ownerSurrenderReason = await _repository.Get(id);
        await _repository.Delete(ownerSurrenderReason);
        return true;
    }
}