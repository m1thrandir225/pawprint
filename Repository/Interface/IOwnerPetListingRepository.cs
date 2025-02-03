using Domain;

namespace Repository.Interface;

public interface IOwnerPetListingRepository : ICrudRepository<OwnerPetListing>
{
    public List<OwnerPetListing> FilterListingsByOwner(Guid ownerId);

    List<OwnerPetListing> FilterListings(Guid? petTypeId, Guid? petSizeId, Guid? petGenderId, string? search);

    ICollection<OwnerPetListing> FilterByStatus(Guid adoptionStatusId, Guid ownerId);
}