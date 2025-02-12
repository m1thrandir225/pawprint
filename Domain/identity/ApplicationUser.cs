using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Domain.identity;

public class ApplicationUser : IdentityUser<Guid>
{
    [Column("address", TypeName="TEXT")]
    public string Address { get; set; }

    [Column("created_at", TypeName = "TIMESTAMPTZ")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}