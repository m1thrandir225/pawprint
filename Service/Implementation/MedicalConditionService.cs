using Domain;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.MedicalCondition;
using Repository.Interface;

namespace Service.Implementation;

public class MedicalConditionService : IMedicalConditionService
{
    private readonly IMedicalConditionRepository _repository;

    public MedicalConditionService(IMedicalConditionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MedicalCondition>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<MedicalCondition> GetByIdAsync(Guid id)
    {
        var medicalCondition = await _repository.Get(id);
        return medicalCondition;
    }

    public async Task<MedicalCondition> CreateAsync(CreateMedicalConditionRequest dto)
    {
        var medicalCondition = new MedicalCondition(dto.MedicalRecordId, dto.ConditionName, dto.Notes);
        return await _repository.Insert(medicalCondition);
    }

    public async Task<MedicalCondition> UpdateAsync(Guid id, UpdateMedicalConditionRequest dto)
    {
        var medicalCondition = await _repository.Get(id);

        if (medicalCondition == null)
        {
            return null;
        }

        medicalCondition.MedicalRecordId = dto.MedicalRecordId;
        medicalCondition.ConditionName = dto.ConditionName;
        medicalCondition.Notes = dto.Notes;

        return  await _repository.Update(medicalCondition);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var medicalCondition = await _repository.Get(id);
        await _repository.Delete(medicalCondition);
        return true;
    }
}