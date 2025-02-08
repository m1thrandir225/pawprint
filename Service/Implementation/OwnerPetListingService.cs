using Domain.DTOs.OwnerPetListing;

namespace Service.Implementation;

using Domain;
using Domain.DTOs;
using Repository.Interface;
using Service.Interface;

public class OwnerPetListingService : IOwnerPetListingService
{
    private readonly IOwnerPetListingRepository _repository;
    private readonly IEmailService _emailService;

    public OwnerPetListingService(IOwnerPetListingRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
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
        var createdListing = _repository.Insert(ownerPetListing);

        return createdListing;
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

    public List<OwnerPetListing> FilterListingsByOwner(Guid ownerId)
    {
        return _repository.FilterListingsByOwner(ownerId);
    }

    public List<OwnerPetListing> FilterShelterPetListing(Guid? petSizeId, Guid? petTypeId, Guid? petGenderId, string search)
    {
        return _repository.FilterListings(petTypeId, petSizeId, petGenderId, search);
    }

    public ICollection<OwnerPetListing> FilterByStatus(Guid adoptionStatusId, Guid ownerId)
    {
        return _repository.FilterByStatus(adoptionStatusId, ownerId);
    }
}