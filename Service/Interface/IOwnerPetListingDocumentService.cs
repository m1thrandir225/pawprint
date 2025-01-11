using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IOwnerPetListingDocumentService : ICRUDService<OwnerPetListingDocument, 
    CreateOwnerPetListingDocumentRequest, UpdateOwnerPetListingDocumentRequest>
{
}