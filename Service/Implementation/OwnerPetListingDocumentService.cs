using Domain;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.OwnerPetListingDocument;
using Repository.Interface;

namespace Service.Implementation;

public class OwnerPetListingDocumentService : IOwnerPetListingDocumentService
{
    private readonly IOwnerPetListingDocumentRepository _repository;

    public OwnerPetListingDocumentService(IOwnerPetListingDocumentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OwnerPetListingDocument>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<OwnerPetListingDocument> GetByIdAsync(Guid id)
    {
        var document = await _repository.Get(id);
        return document;
    }

    public async Task<OwnerPetListingDocument> CreateAsync(CreateOwnerPetListingDocumentRequest dto)
    {
        var document = new OwnerPetListingDocument(
            dto.ListingId,
            dto.DocumentUrl,
            dto.DocumentType
        );

        return await _repository.Insert(document);
    }

    public async Task<OwnerPetListingDocument> UpdateAsync(Guid id, UpdateOwnerPetListingDocumentRequest dto)
    {
        var document = await _repository.Get(id);

        if (document == null)
        {
            return null;
        }

        document.DocumentUrl = dto.DocumentUrl;
        document.DocumentType = dto.DocumentType;

        return await _repository.Update(document);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var document = await _repository.Get(id);
        
        await _repository.Delete(document);
        return true;
    }
}