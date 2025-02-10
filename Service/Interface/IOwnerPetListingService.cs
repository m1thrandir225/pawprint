using Domain.DTOs.OwnerPetListing;

namespace Service.Interface;

using Domain;
using Domain.DTOs;

public interface IOwnerPetListingService : ICRUDService<OwnerPetListing, CreateOwnerPetListingRequest, UpdateOwnerPetListingRequest>
{
    public List<OwnerPetListing> FilterListingsByOwner(Guid ownerId);

    List<OwnerPetListing> FilterShelterPetListing(Guid? petSizeId, Guid? petTypeId, Guid? petGenderId, string search);

    ICollection<OwnerPetListing> FilterByStatus(Guid adoptionStatusId, Guid ownerId);
}