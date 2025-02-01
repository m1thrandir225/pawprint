using Domain;

namespace Repository.Interface;

public interface IShelterPetListingRepository : ICrudRepository<ShelterPetListing>
{
    public List<ShelterPetListing> GetListingByShelter(Guid shelterId);

    public List<ShelterPetListing> FilterListings(Guid? petTypeId, Guid? petSizeId, Guid? petGenderId, string? search);

}