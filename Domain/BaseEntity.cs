using System.ComponentModel.DataAnnotations;

namespace Domain;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

}