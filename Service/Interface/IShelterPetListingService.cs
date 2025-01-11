using Domain;
using Domain.DTOs;
using Domain.enums;

namespace Service.Interface;

public interface IShelterPetListingService : ICRUDService<ShelterPetListing, 
    CreateShelterPetListingRequest, UpdateShelterPetListingRequest>
{
    // Additional method for handling approval status changes
    Task<ShelterPetListing> UpdateApprovalStatusAsync(Guid id, ApprovalStatus status);
}