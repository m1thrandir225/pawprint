namespace Service.Implementation;

using Domain;
using Domain.DTOs;
using Repository.Interface;
using Service.Interface;

public class OwnerPetListingService : IOwnerPetListingService
{
    private readonly IOwnerPetListingRepository _repository;

    public OwnerPetListingService(IOwnerPetListingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OwnerPetListing>> GetAllAsync()
    {
        return _repository.GetAll();
    }

    public async Task<OwnerPetListing> GetByIdAsync(Guid id)
    {
        return _repository.Get(id);
    }

    public async Task<OwnerPetListing> CreateAsync(CreateOwnerPetListingRequest dto)
    {
        var ownerPetListing = new OwnerPetListing(dto.AdopterId, dto.PetId, dto.SurrenderReasonId, dto.AdoptionFee);
        return _repository.Insert(ownerPetListing);
    }

    public async Task<OwnerPetListing> UpdateAsync(Guid id, UpdateOwnerPetListingRequest dto)
    {
        var ownerPetListing = _repository.Get(id);
        if (ownerPetListing == null)
        {
            return null;
        }

        ownerPetListing.AdopterId = dto.AdopterId;
        ownerPetListing.PetId = dto.PetId;
        ownerPetListing.SurrenderReasonId = dto.SurrenderReasonId;
        ownerPetListing.AdoptionFee = dto.AdoptionFee;

        return _repository.Update(ownerPetListing);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var ownerPetListing = _repository.Get(id);
        if (ownerPetListing == null) return Task.FromResult(false);
        _repository.Delete(ownerPetListing);
        return Task.FromResult(true);
    }

    public List<OwnerPetListing> GetListingsByOwner(Guid ownerId)
    {
        return _repository.GetListingsByOwner(ownerId);
    }
}