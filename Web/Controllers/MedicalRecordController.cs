using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;

namespace Web.Controllers
{
    [Route("api/medical-records")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetAllMedicalRecords()
        {
            var medicalRecords = await _medicalRecordService.GetAllAsync();
            if (medicalRecords == null)
            {
                return BadRequest();
            }

            return Ok(medicalRecords);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord([FromRoute] Guid id)
        {
            var medicalRecord = await _medicalRecordService.GetByIdAsync(id);

            if (medicalRecord == null)
            {
                return NotFound();
            }
            return Ok(medicalRecord);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> CreateMedicalRecord([FromBody] CreateMedicalRecordRequest request)
        {
            var medicalRecord = await _medicalRecordService.CreateAsync(request);

            return Ok(medicalRecord);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<MedicalRecord>> UpdateMedicalRecord([FromBody] UpdateMedicalRecordRequest request)
        {
            var updated = await _medicalRecordService.UpdateAsync(request.Id, request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<MedicalRecord>> DeleteMedicalRecord([FromRoute] Guid id)
        {
            var deleted = await _medicalRecordService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }
    }
}
