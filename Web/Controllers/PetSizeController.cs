using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;

namespace Web.Controllers
{
    [Route("api/pet-size")]
    [ApiController]
    public class PetSizeController : ControllerBase
    {
        private readonly IPetSizeService _petSizeService;

        public PetSizeController(IPetSizeService petSizeService)
        {
            _petSizeService = petSizeService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<PetSize>>> GetAllPetSizes()
        {
            var petSize = await _petSizeService.GetAllAsync();
            if (petSize == null)
            {
                return BadRequest();
            }

            return Ok(petSize);
        }

        [HttpGet]
        [Route("{id:guid}")]
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
        public async Task<ActionResult<PetSize>> CreatePetSize([FromBody] CreatePetSizeRequest request)
        {
            var petSize = await _petSizeService.CreateAsync(request);

            return Ok(petSize);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<PetSize>> UpdatePetSize([FromBody] UpdatePetSizeRequest request, [FromRoute] Guid id)
        {
            var updated = await _petSizeService.UpdateAsync(id ,request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);

        }

        [HttpDelete]
        [Route("{id:guid}")]
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
