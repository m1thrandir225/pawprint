using Domain;
using Domain.DTOs;
using Domain.DTOs.Vaccination;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class VaccinationService : IVaccinationService
{
    private readonly IVaccinationRepository _repository;

    public VaccinationService(IVaccinationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Vaccination>> GetAllAsync()
    {
        return await _repository.GetAll();
    }

    public async Task<Vaccination> GetByIdAsync(Guid id)
    {
        return await _repository.Get(id);
    }

    public async Task<Vaccination> CreateAsync(CreateVaccinationRequest dto)
    {
        var vaccination = new Vaccination(dto.MedicalRecordId, dto.VaccinationName, dto.VaccineDate);
        
        return await _repository.Insert(vaccination);
    }

    public async Task<Vaccination> UpdateAsync(Guid id, UpdateVaccinationRequest dto)
    {
        var vaccination = await _repository.Get(id);

        if (vaccination == null)
        {
            return null;
        }
        
        vaccination.MedicalRecordId = dto.MedicalRecordId;
        vaccination.VaccineName = dto.VaccinationName;
        vaccination.VaccineDate = dto.VaccineDate;
        
        return await _repository.Update(vaccination);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var vaccination = await _repository.Get(id);
        
        await _repository.Delete(vaccination);
        return true;
    }
}