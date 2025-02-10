using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

[Table("veterinarians")]
public class Veterinarian : BaseEntity
{
    [Required] public string Name { get; set; }

    [Required] public string ClinicName { get; set; }

    [Required] public string ContactNumber { get; set; }

    [EmailAddress] public string Email { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<VeterinarianSpecilization> VetSpecializations { get; set; }
    // public virtual ICollection<MedicalRecord> MedicalRecords { get; set; }

    // public Veterinarian()
    // {
    // }
    //
    // public Veterinarian(
    //     string name,
    //     string contactNumber,
    //     string email,
    //     string clinicName
    //     )
    // {
    //     Name = name;
    //     ContactNumber = contactNumber;
    //     Email = email;
    //     ClinicName = clinicName;
    // }
}
