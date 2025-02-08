using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.PetSize;
using Domain.identity;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Route("api/pet-sizes")]
    [ApiController]
    public class PetSizeController : ControllerBase
    {
        private readonly IPetSizeService _petSizeService;

        public PetSizeController(IPetSizeService petSizeService)
        {
            _petSizeService = petSizeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetSize>>> GetAllPetSizes()
        {
            var petSize = await _petSizeService.GetAllAsync();
            if (petSize == null)
            {
                return BadRequest();
            }

            return Ok(petSize);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PetSize>> GetPetSize([FromRoute] Guid id)
        {
            var petSize = await _petSizeService.GetByIdAsync(id);

            if (petSize == null)
            {
                return NotFound();
            }
            return Ok(petSize);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRole.Admin}")]
        public async Task<ActionResult<PetSize>> CreatePetSize([FromBody] CreatePetSizeRequest request)
        {
            var petSize = await _petSizeService.CreateAsync(request);

            return Ok(petSize);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = $"{UserRole.Admin}")]
        public async Task<ActionResult<PetSize>> UpdatePetSize([FromBody] UpdatePetSizeRequest request, [FromRoute] Guid id)
        {
            var updated = await _petSizeService.UpdateAsync(id ,request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);

        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = $"{UserRole.Admin}")]
        public async Task<ActionResult<PetSize>> DeletePetSize([FromRoute] Guid id)
        {
            var deleted = await _petSizeService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }

    }
}
