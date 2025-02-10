using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.Veterinarian;
using Domain.identity;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Route("api/veterinarians")]
    [ApiController]
    public class VeterinarianController : ControllerBase
    {
        private readonly IVeterinarianService _veterinarianService;

        public VeterinarianController(IVeterinarianService veterinarianService)
        {
            _veterinarianService = veterinarianService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veterinarian>>> GetAllVeterinarians()
        {
            var veterinarians = await _veterinarianService.GetAllAsync();
            if (veterinarians == null)
            {
                return BadRequest();
            }

            return Ok(veterinarians);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<Veterinarian>> GetVeterinarian([FromRoute] Guid id)
        {
            var veterinarian = await _veterinarianService.GetByIdAsync(id);

            if (veterinarian == null)
            {
                return NotFound();
            }
            return Ok(veterinarian);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        public async Task<ActionResult<Veterinarian>> CreateVeterinarian([FromBody] CreateVeterinarianRequest request)
        {
            var veterinarian = await _veterinarianService.CreateAsync(request);

            return Ok(veterinarian);
        }

        [HttpPut]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        [Route("{id:guid}")]
        public async Task<ActionResult<Veterinarian>> UpdateVeterinarian([FromBody] UpdateVeterinarianRequest request, [FromRoute] Guid id)
        {
            var updated = await _veterinarianService.UpdateAsync(id ,request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);

        }

        [HttpDelete]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        [Route("{id:guid}")]
        public async Task<ActionResult<Veterinarian>> DeleteVeterinarian([FromRoute] Guid id)
        {
            var deleted = await _veterinarianService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }

    }
}
