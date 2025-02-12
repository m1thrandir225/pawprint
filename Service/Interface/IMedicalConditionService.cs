using Domain;
using Domain.DTOs;
using Domain.DTOs.MedicalCondition;

namespace Service.Interface;

public interface IMedicalConditionService : ICRUDService<MedicalCondition, CreateMedicalConditionRequest, UpdateMedicalConditionRequest>
{
}