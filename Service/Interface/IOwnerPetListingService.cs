namespace Service.Interface;

using Domain;
using Domain.DTOs;

public interface IOwnerPetListingService : ICRUDService<OwnerPetListing, CreateOwnerPetListingRequest, UpdateOwnerPetListingRequest>
{
}