using Domain;

namespace Repository.Interface;

public interface IShelterPetListingRepository : ICrudRepository<ShelterPetListing>
{
    public List<ShelterPetListing> GetListingByShelter(Guid shelterId);

}