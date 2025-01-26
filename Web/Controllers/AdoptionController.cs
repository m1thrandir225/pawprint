using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;

namespace Web.Controllers
{
    [Route("api/adoptions")]
    [ApiController]
    public class AdoptionController : ControllerBase
    {
        private readonly IAdoptionService _adoptionService;

        public AdoptionController(IAdoptionService adoptionService)
        {
            _adoptionService = adoptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adoption>>> GetAllAdoptions()
        {
            var adoptions = await _adoptionService.GetAllAsync();
            if (adoptions == null)
            {
                return BadRequest();
            }

            return Ok(adoptions);
        }

        [HttpGet("pet/{id:guid}")]
        public List<Adoption> GetAdoptionsForPet([FromRoute] Guid id)
        {
            return _adoptionService.GetAdoptionsForPet(id);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ActionResult<Adoption>> GetAdoption([FromRoute] Guid id)
        {
            var adoption = await _adoptionService.GetByIdAsync(id);

            if (adoption == null)
            {
                return NotFound();
            }
            return Ok(adoption);
        }

        [HttpPost]
        public async Task<ActionResult<Adoption>> CreateAdoption([FromBody] CreateAdoptionRequest request)
        {
            var adoption = await _adoptionService.CreateAsync(request);

            return Ok(adoption);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<Adoption>> UpdateAdoption([FromBody] UpdateAdoptionRequest request, [FromRoute] Guid id)
        {
            var updated = await _adoptionService.UpdateAsync(id ,request);
            if (updated == null)
            {
                return BadRequest();
            }
            return Ok(updated);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult<Adoption>> DeleteAdoption([FromRoute] Guid id)
        {
            var deleted = await _adoptionService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }
            return Ok(deleted);
        }

    }
}
