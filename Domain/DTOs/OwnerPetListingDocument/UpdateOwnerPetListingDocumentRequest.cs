namespace Domain.DTOs.OwnerPetListingDocument;

public class UpdateOwnerPetListingDocumentRequest
{
    public Guid Id { get; set; }
    public string DocumentUrl { get; set; }
    public string DocumentType { get; set; }

    public UpdateOwnerPetListingDocumentRequest(Guid id, string documentUrl, string documentType)
    {
        Id = id;
        DocumentUrl = documentUrl;
        DocumentType = documentType;
    }
}