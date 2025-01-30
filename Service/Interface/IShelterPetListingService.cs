using Domain;
using Domain.DTOs;
using Domain.enums;

namespace Service.Interface;

public interface IShelterPetListingService : ICRUDService<ShelterPetListing, 
    CreateShelterPetListingRequest, UpdateShelterPetListingRequest>
{
    List<ShelterPetListing> FilterShelterPetListing(Guid? petSizeId, Guid? petTypeId, Guid? petGenderId, string search);

    // Additional method for handling approval status changes
    Task<ShelterPetListing> UpdateApprovalStatusAsync(Guid id, ApprovalStatus status);

    List<ShelterPetListing> GetListingsByShelter(Guid shelterId);

}