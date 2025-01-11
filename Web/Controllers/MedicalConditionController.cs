using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;

namespace Web.Controllers
{
    [Route("api/medical-conditions")]
    [ApiController]
    public class MedicalConditionController : ControllerBase
    {
        private readonly IMedicalConditionService _medicalConditionService;

        public MedicalConditionController(IMedicalConditionService medicalConditionService)
        {
            _medicalConditionService = medicalConditionService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<MedicalCondition>>> GetAllMedicalConditions()
        {
            var medicalConditions = await _medicalConditionService.GetAllAsync();
            if (medicalConditions == null)
            {
                return BadRequest();
            }

            return Ok(medicalConditions);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<MedicalCondition>> GetMedicalCondition([FromRoute] Guid id)
        {
            var medicalCondition = await _medicalConditionService.GetByIdAsync(id);

            if (medicalCondition == null)
            {
                return NotFound();
            }
            return Ok(medicalCondition);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalCondition>> CreateMedicalCondition([FromBody] CreateMedicalConditionRequest request)
        {
            var medicalCondition = await _medicalConditionService.CreateAsync(request);

            return Ok(medicalCondition);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<MedicalCondition>> UpdateMedicalCondition([FromBody] UpdateMedicalConditionRequest request)
        {
            var updated = await _medicalConditionService.UpdateAsync(request.Id ,request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<MedicalCondition>> DeleteMedicalCondition([FromRoute] Guid id)
        {
            var deleted = await _medicalConditionService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }
    }
}
