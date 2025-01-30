using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Route("api/vaccinations")]
    [ApiController]
    public class VaccinationController : ControllerBase
    {
        private readonly IVaccinationService _vaccinationService;

        public VaccinationController(IVaccinationService vaccinationService)
        {
            _vaccinationService = vaccinationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vaccination>>> GetAllVaccinations()
        {
            var vaccinations = await _vaccinationService.GetAllAsync();
            if (vaccinations == null)
            {
                return BadRequest();
            }

            return Ok(vaccinations);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<Vaccination>> GetVaccination([FromRoute] Guid id)
        {
            var vaccination = await _vaccinationService.GetByIdAsync(id);

            if (vaccination == null)
            {
                return NotFound();
            }
            return Ok(vaccination);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        public async Task<ActionResult<Vaccination>> CreateVaccination([FromBody] CreateVaccinationRequest request)
        {
            var vaccination = await _vaccinationService.CreateAsync(request);

            return Ok(vaccination);
        }

        [HttpPut]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        [Route("{id:guid}")]
        public async Task<ActionResult<Vaccination>> UpdateVaccination([FromBody] UpdateVaccinationRequest request, [FromRoute] Guid id)
        {
            var updated = await _vaccinationService.UpdateAsync(id ,request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);

        }

        [HttpDelete]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        [Route("{id:guid}")]
        public async Task<ActionResult<Vaccination>> DeleteVaccination([FromRoute] Guid id)
        {
            var deleted = await _vaccinationService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }

    }
}
