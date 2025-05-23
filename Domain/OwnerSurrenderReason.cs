﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain;

[Table("owner_surrender_reasons")]
public class OwnerSurrenderReason : BaseEntity
{
    [Required]
    [Column("description", TypeName = "TEXT")]
    public string Description { get; set; }
    
    public OwnerSurrenderReason()
    {
    }
    
    public OwnerSurrenderReason(string description)
    {
        Id = Guid.NewGuid(); 
        Description = description;
    }
}