using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IMedicalConditionService : ICRUDService<MedicalCondition, CreateMedicalConditionRequest, UpdateMedicalConditionRequest>
{
}