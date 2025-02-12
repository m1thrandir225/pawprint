using Domain;
using Domain.DTOs;
using Domain.DTOs.ShelterPetListing;
using Domain.enums;

namespace Service.Interface;

public interface IShelterPetListingService : ICRUDService<ShelterPetListing, 
    CreateShelterPetListingDTO, UpdateShelterPetListingRequest>
{
    ICollection<ShelterPetListing> FilterShelterPetListing(Guid? petSizeId, Guid? petTypeId, Guid? petGenderId, string search);

    // Additional method for handling approval status changes
    // Task<ShelterPetListing> UpdateApprovalStatusAsync(Guid id, ApprovalStatus status);

    ICollection<ShelterPetListing> GetListingsByShelter(Guid shelterId);

    ICollection<ShelterPetListing> FilterByStatus(Guid adoptionStatusId, Guid shelterId);

}