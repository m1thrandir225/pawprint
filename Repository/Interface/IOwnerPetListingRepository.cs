using Domain;

namespace Repository.Interface;

public interface IOwnerPetListingRepository : ICrudRepository<OwnerPetListing>
{
    public List<OwnerPetListing> GetListingsByOwner(Guid ownerId);
}