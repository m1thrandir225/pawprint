using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.PetGender;
using Domain.identity;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Route("api/pet-genders")]
    [ApiController]
    public class PetGenderController : ControllerBase
    {
        private readonly IPetGenderService _petGenderService;

        public PetGenderController(IPetGenderService petGenderService)
        {
            _petGenderService = petGenderService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetGender>>> GetAllPetGenders()
        {
            var petGenders = await _petGenderService.GetAllAsync();
            if (petGenders == null)
            {
                return BadRequest();
            }

            return Ok(petGenders);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PetGender>> GetPetGender([FromRoute] Guid id)
        {
            var petGenders = await _petGenderService.GetByIdAsync(id);

            if (petGenders == null)
            {
                return NotFound();
            }

            return Ok(petGenders);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRole.Admin}")]
        public async Task<ActionResult<PetGender>> CreatePetGender([FromBody] CreatePetGenderRequest request)
        {
            var petGender = await _petGenderService.CreateAsync(request);

            return Ok(petGender);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = $"{UserRole.Admin}")]
        public async Task<ActionResult<PetGender>> UpdatePetGender([FromRoute] Guid id, [FromBody] UpdatePetGenderRequest request)
        {
            var updated = await _petGenderService.UpdateAsync(id, request);
            if (updated == null)
            {
                return BadRequest();
            }

            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = $"{UserRole.Admin}")]
        public async Task<ActionResult<PetGender>> DeletePetGender([FromRoute] Guid id)
        {
            var deleted = await _petGenderService.DeleteAsync(id);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok(deleted);
        }
    }
}