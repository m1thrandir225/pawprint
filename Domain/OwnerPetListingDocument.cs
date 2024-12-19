using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class OwnerPetListingDocument : BaseEntity
{
    [ForeignKey(nameof(Listing))]
    [Column("listing_id", TypeName = "UUID")]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Guid ListingId { get; set; }

    [Required]
    public OwnerPetListing Listing { get; set; } = null!;

    [Required]
    [Column("document_url", TypeName = "TEXT")]
    public string DocumentUrl { get; set; }

    [Required]
    [Column("document_type", TypeName = "TEXT")]
    public string DocumentType { get; set; }

    [Required]
    [Column("uploaded_at", TypeName = "TIMESTAMPTZ")]
    public DateTime UploadedAt { get; set; } = DateTime.Now;

}