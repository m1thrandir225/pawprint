using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.VeterinarianSpecialization;
using Domain.identity;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Route("api/veterinarian-specializations")]
    [ApiController]
    public class VeterinarianSpecializationController : ControllerBase
    {
        private readonly IVeterinarianSpecializationService _specializationService;

        public VeterinarianSpecializationController(IVeterinarianSpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeterinarianSpecilization>>> GetAllSpecializations()
        {
            var specializations = await _specializationService.GetAllAsync();
            if (specializations == null)
            {
                return BadRequest();
            }

            return Ok(specializations);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<VeterinarianSpecilization>> GetSpecialization([FromRoute] Guid id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);

            if (specialization == null)
            {
                return NotFound();
            }
            return Ok(specialization);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        public async Task<ActionResult<VeterinarianSpecilization>> CreateSpecialization([FromBody] CreateVeterinarianSpecializationRequest request)
        {
            var specialization = await _specializationService.CreateAsync(request);

            return Ok(specialization);
        }

        [HttpPut]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        [Route("{id}")]
        public async Task<ActionResult<VeterinarianSpecilization>> UpdateSpecialization([FromBody] UpdateVeterinarianSpecializationRequest request)
        {
            var updated = await _specializationService.UpdateAsync(request.Id, request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);
        }

        [HttpDelete]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        [Route("{id}")]
        public async Task<ActionResult<bool>> DeleteSpecialization([FromRoute] Guid id)
        {
            var deleted = await _specializationService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }
    }
}
