using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain;

[Table("owner_surrender_reasons")]
public class OwnerSurrenderReason : BaseEntity
{
    [Required]
    [Column("description", TypeName = "VARCHAR(255)")]
    public string Description { get; set; }
}