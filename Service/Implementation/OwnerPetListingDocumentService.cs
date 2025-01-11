using Domain;
using Service.Interface;
using Domain.DTOs;
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
        return _repository.GetAll();
    }

    public async Task<OwnerPetListingDocument> GetByIdAsync(Guid id)
    {
        var document = _repository.Get(id);
        return document;
    }

    public async Task<OwnerPetListingDocument> CreateAsync(CreateOwnerPetListingDocumentRequest dto)
    {
        var document = new OwnerPetListingDocument(
            dto.ListingId,
            dto.DocumentUrl,
            dto.DocumentType
        );

        return _repository.Insert(document);
    }

    public async Task<OwnerPetListingDocument> UpdateAsync(Guid id, UpdateOwnerPetListingDocumentRequest dto)
    {
        var document = _repository.Get(id);

        if (document == null)
        {
            return null;
        }

        document.DocumentUrl = dto.DocumentUrl;
        document.DocumentType = dto.DocumentType;

        return _repository.Update(document);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var document = _repository.Get(id);
        
        _repository.Delete(document);
        return Task.FromResult(true);
    }
}