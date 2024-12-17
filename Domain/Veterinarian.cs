using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("veterinarians")]
public class Veterinarian : BaseEntity
{
    [Required]
    [Column("name", TypeName = "VARCHAR(50)")]
    public string Name { get; set; }

    [Required]
    [Column("clinic_name", TypeName = "VARCHAR(100)")]
    public string ClinicName { get; set; }

    [Required]
    [Column("contact_number", TypeName = "VARCHAR(100)")]
    public string ContactNumber { get; set; }

    [Column("email", TypeName = "VARCHAR(100)")]
    [EmailAddress]
    public string Email { get; set; }

    [Column("created_at", TypeName = "TIMESTAMPTZ")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<VeterinarianSpecilization> Specializations { get; } = new();

    public List<MedicalRecord> MedicalRecords { get; } = new();
}