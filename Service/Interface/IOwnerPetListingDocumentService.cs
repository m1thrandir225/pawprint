using Domain;
using Domain.DTOs;
using Domain.DTOs.OwnerPetListingDocument;

namespace Service.Interface;

public interface IOwnerPetListingDocumentService : ICRUDService<OwnerPetListingDocument, 
    CreateOwnerPetListingDocumentRequest, UpdateOwnerPetListingDocumentRequest>
{
}