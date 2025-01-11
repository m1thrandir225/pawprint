using Domain;
using Domain.DTOs;

namespace Service.Interface;

public interface IMedicalRecordService : ICRUDService<MedicalRecord, CreateMedicalRecordRequest, UpdateMedicalRecordRequest>
{
}