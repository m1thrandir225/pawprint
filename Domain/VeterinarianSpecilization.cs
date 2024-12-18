using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain;

[Table("vet_specializations")]
public class VeterinarianSpecilization : BaseEntity
{
    [Required]
    [Column("vet_id", TypeName = "UUID")]
    [ForeignKey(nameof(Veterinarian))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public Guid VeterinarianId { get; set; }

    [Required]
    [Column("specialization", TypeName = "VARCHAR(255)")]
    public string Specialization { get; set; }

    [Required]
    public Veterinarian Veterinarian { get; init; } = null!;
}