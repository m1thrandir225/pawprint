using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;

namespace Web.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<Pet>>> GetAllPets()
        {
            var pets = await _petService.GetAllAsync();
            if (pets == null)
            {
                return BadRequest();
            }

            return Ok(pets);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<Pet>> GetPet([FromRoute] Guid id)
        {
            var pet = await _petService.GetByIdAsync(id);

            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> CreatePet([FromBody] CreatePetRequest request)
        {
            var pet = await _petService.CreateAsync(request);

            return Ok(pet);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<Pet>> UpdatePet([FromBody] UpdatePetRequest request, [FromRoute] Guid id)
        {
            var updated = await _petService.UpdateAsync(id ,request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult<Pet>> DeletePet([FromRoute] Guid id)
        {
            var deleted = await _petService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }

    }
}
