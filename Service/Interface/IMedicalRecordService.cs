using Domain;
using Domain.DTOs;
using Domain.DTOs.MedicalRecord;

namespace Service.Interface;

public interface IMedicalRecordService : ICRUDService<MedicalRecord, CreateMedicalRecordRequest, UpdateMedicalRecordRequest>
{
}