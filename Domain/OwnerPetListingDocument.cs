using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain;

[Table("owner_pet_listing_documents")]
public class OwnerPetListingDocument : BaseEntity
{
    [Required]
    [Column("listing_id", TypeName = "UUID")]
    [ForeignKey(nameof(Listing))]
    public Guid ListingId { get; set; }

    [JsonIgnore]
    public virtual OwnerPetListing Listing { get; set; } = null!;

    [Required]
    [Column("document_url", TypeName = "TEXT")]
    public string DocumentUrl { get; set; }

    [Required]
    [Column("document_type", TypeName = "TEXT")]
    public string DocumentType { get; set; }

    [Required]
    [Column("uploaded_at", TypeName = "TIMESTAMPTZ")]
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    
    public OwnerPetListingDocument()
    {
    }
    
    public OwnerPetListingDocument(Guid listingId, string documentUrl, string documentType)
    {
        Id = Guid.NewGuid();
        ListingId = listingId;
        DocumentUrl = documentUrl;
        DocumentType = documentType;
        UploadedAt = DateTime.UtcNow;
    }
}