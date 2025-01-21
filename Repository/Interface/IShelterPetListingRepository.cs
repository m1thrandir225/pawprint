using Domain;

namespace Repository.Interface;

public interface IShelterPetListingRepository : ICrudRepository<ShelterPetListing>
{
    public IEnumerable<ShelterPetListing> GetAllWithJoins();
    public ShelterPetListing GetWithJoins(Guid id);
}