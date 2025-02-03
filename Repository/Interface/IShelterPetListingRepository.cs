using Domain;
using Domain.enums;

namespace Repository.Interface;

public interface IShelterPetListingRepository : ICrudRepository<ShelterPetListing>
{
    public ICollection<ShelterPetListing> GetListingByShelter(Guid shelterId);

    public ICollection<ShelterPetListing> FilterListings(Guid? petTypeId, Guid? petSizeId, Guid? petGenderId, string? search);

    public ICollection<ShelterPetListing> FilterByStatus(ApprovalStatus status, Guid shelterId);

}