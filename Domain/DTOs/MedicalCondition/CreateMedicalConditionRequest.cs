﻿namespace Domain.DTOs.MedicalCondition;

public class CreateMedicalConditionRequest
{
    public Guid MedicalRecordId { get; set; }
    public string ConditionName { get; set; }
    public string? Notes { get; set; }
}