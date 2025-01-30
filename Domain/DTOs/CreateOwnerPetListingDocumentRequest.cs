namespace Domain.DTOs;

public class CreateOwnerPetListingDocumentRequest
{
    public Guid ListingId { get; set; }
    public string DocumentUrl { get; set; }
    public string DocumentType { get; set; }
    public float AdoptionFee { get; set; }
}