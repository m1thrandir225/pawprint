using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;

namespace Web.Controllers
{
    [Route("api/pet-types")]
    [ApiController]
    public class PetTypeController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;

        public PetTypeController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<PetType>>> GetAllPetTypes()
        {
            var petTypes = await _petTypeService.GetAllAsync();
            if (petTypes == null)
            {
                return BadRequest();
            }

            return Ok(petTypes);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<PetType>> GetPetType([FromRoute] Guid id)
        {
            var petType = await _petTypeService.GetByIdAsync(id);

            if (petType == null)
            {
                return NotFound();
            }
            return Ok(petType);
        }

        [HttpPost]
        public async Task<ActionResult<PetType>> CreatePetType([FromBody] CreatePetTypeRequest request)
        {
            var petType = await _petTypeService.CreateAsync(request);

            return Ok(petType);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<PetType>> UpdatePetType([FromBody] UpdatePetTypeRequest request)
        {
            var updated = await _petTypeService.UpdateAsync(request.Id ,request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<PetType>> DeletePetType([FromRoute] Guid id)
        {
            var deleted = await _petTypeService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }

    }
}
