using System.ComponentModel.DataAnnotations;

namespace Domain.integration;

public class Restaurant : BaseEntity
{
    [Required] public string Name { get; set; } = string.Empty;

    [Required] public string Address { get; set; }

    [Required] public string PhoneNumber { get; set; }

    [Required] public string Description { get; set; }

    [Required] public string Image { get; set; }

    [Required] public double Rating { get; set; }
}