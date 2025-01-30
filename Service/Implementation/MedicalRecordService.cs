using Domain;
using Service.Interface;
using Domain.DTOs;
using Repository.Interface;

namespace Service.Implementation;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IMedicalRecordRepository _repository;

    public MedicalRecordService(IMedicalRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MedicalRecord>> GetAllAsync()
    {
        return _repository.GetAll();
    }

    public async Task<MedicalRecord> GetByIdAsync(Guid id)
    {
        var medicalRecord = _repository.Get(id);
        return medicalRecord;
    }

    public async Task<MedicalRecord> CreateAsync(CreateMedicalRecordRequest dto)
    {
        var medicalRecord = new MedicalRecord
        (
            dto.VetId,
            dto.SpayNeuterStatus,
            dto.LastMedicalCheckup,
            dto.MicrochipNumber
        );

        return _repository.Insert(medicalRecord);
    }

    public async Task<MedicalRecord> UpdateAsync(Guid id, UpdateMedicalRecordRequest dto)
    {
        var medicalRecord = _repository.Get(id);

        if (medicalRecord == null)
        {
            return null;
        }

        medicalRecord.VetId = dto.VetId;
        medicalRecord.SpayNeuterStatus = dto.SpayNeuterStatus;
        medicalRecord.LastMedicalCheckup = dto.LastMedicalCheckup;
        medicalRecord.MicrochipNumber = dto.MicrochipNumber;

        return _repository.Update(medicalRecord);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var medicalRecord = _repository.Get(id);
        _repository.Delete(medicalRecord);
        return Task.FromResult(true);
    }
}