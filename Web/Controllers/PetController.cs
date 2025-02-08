using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Domain.DTOs;
using Domain.DTOs.Pet;
using Domain.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IUploadService _uploadService;

        public PetController(IPetService petService, IUploadService uploadService)
        {
            _petService = petService;
            _uploadService = uploadService;
        }

        [HttpGet]
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
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        public async Task<ActionResult<Pet>> CreatePet([FromForm] CreatePetRequest request, [FromForm] IFormFile avatar,
            [FromForm] List<IFormFile>? images)
        {
            if (avatar == null)
            {
                return BadRequest("Need at least one image");
            }

            // Upload the avatar image
            request.AvatarImg = await _uploadService.UploadFile(avatar);

            // Initialize ImageShowcase if null
            var imageShowcase = new List<string>();

            // Upload other images and add to ImageShowcase
            if (images != null)
            {
                foreach (var image in images)
                {
                    imageShowcase.Add(await _uploadService.UploadFile(image));
                }
            }

            // Set the ImageShowcase property
            request.ImageShowcase = imageShowcase.ToArray();

            // Create the pet
            var pet = await _petService.CreateAsync(request);

            return Ok(pet);
        }

        [HttpPut]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        [Route("{id:guid}")]
        public async Task<ActionResult<Pet>> UpdatePet([FromForm] UpdatePetDTO dto, [FromRoute] Guid id,
            [FromForm] IFormFile? avatar,
            [FromForm] List<IFormFile>? images)
        {
            if (avatar != null)
            {
                dto.AvatarImg = await _uploadService.ReplaceFile(avatar, dto.AvatarImg);
            }

            if (!images.IsNullOrEmpty())
            {
                if (images.Count == dto.ImageSowcaseUpdate.Length)
                {
                    var imageShowcase = new List<string>(dto.ImageShowcase);

                    // Use Zip to iterate through both images and ImageShowcaseDelete in parallel
                    foreach (var (newImage, oldImage) in images.Zip(dto.ImageSowcaseUpdate))
                    {
                        // Remove the old image
                        imageShowcase.Remove(oldImage);

                        // Replace the old image with the new one
                        var uploadedImage = await _uploadService.ReplaceFile(newImage, oldImage);
                        imageShowcase.Add(uploadedImage);
                    }

                    // Update the ImageShowcase property with the modified list
                    dto.ImageShowcase = imageShowcase.ToArray();
                }
                else
                {
                    return BadRequest("Images and images to update do not match");
                }
            }

            if (!dto.ImageShowcaseDelete.IsNullOrEmpty())
            {
                // Remove files from the storage
                foreach (var image in dto.ImageShowcaseDelete)
                {
                    _uploadService.RemoveFile(image);
                }

                // Update the ImageShowcase to exclude deleted images

                dto.ImageShowcase = dto.ImageShowcase
                    .Where(image => !dto.ImageShowcaseDelete.Contains(image))
                    .ToArray(); // Convert the result back to a string[]
            }


            var updated = await _petService.UpdateAsync(id, dto);

            if (updated == null)
            {
                return BadRequest();
            }

            return Ok(updated);
        }


        [HttpDelete]
        [Authorize(Roles = $"{UserRole.Admin}, {UserRole.Shelter}")]
        [Route("{id:guid}")]
        public async Task<ActionResult<Pet>> DeletePet([FromRoute] Guid id)
        {
            var pet = await _petService.GetByIdAsync(id);

            var avatar = pet.AvatarImg;

            var avatarDeleted = _uploadService.RemoveFile(avatar);

            var images = pet.ImageShowcase;

            if (!images.IsNullOrEmpty())
            {
                foreach (var image in images)
                {
                    _uploadService.RemoveFile(image);
                }
            }

            var deleted = await _petService.DeleteAsync(id);

            if (!deleted || !avatarDeleted)
            {
                return BadRequest();
            }

            return Ok(deleted);
        }
    }
}