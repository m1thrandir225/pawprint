using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class OwnerPetListingDocument : BaseEntity
{
    [ForeignKey(nameof(Listing))]
    [Column("listing_id", TypeName = "UUID")]
    public Guid ListingId { get; set; }

    [Required]
    public virtual OwnerPetListing Listing { get; set; } = null!;

    [Required]
    [Column("document_url", TypeName = "TEXT")]
    public string DocumentUrl { get; set; }

    [Required]
    [Column("document_type", TypeName = "TEXT")]
    public string DocumentType { get; set; }

    [Required]
    [Column("uploaded_at", TypeName = "TIMESTAMPTZ")]
    public DateTime UploadedAt { get; set; } = DateTime.Now;
    
    public OwnerPetListingDocument()
    {
    }
    
    public OwnerPetListingDocument(Guid listingId, string documentUrl, string documentType)
    {
        Id = Guid.NewGuid();
        ListingId = listingId;
        DocumentUrl = documentUrl;
        DocumentType = documentType;
        UploadedAt = DateTime.Now;
    }
}