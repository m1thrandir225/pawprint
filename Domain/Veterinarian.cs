using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("veterinarians")]
public class Veterinarian : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string ClinicName { get; set; }

    [Required]
    public string ContactNumber { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // public List<VeterinarianSpecilization> Specializations { get; } = new();
    // public List<MedicalRecord> MedicalRecords { get; } = new();
    
    public virtual ICollection<VeterinarianSpecilization> VetSpecializations { get; set; }
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }
}