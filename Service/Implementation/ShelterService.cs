using Domain;
using Domain.DTOs;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation
{
    public class ShelterService : IShelterService
    {
        private readonly IShelterRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShelterService(UserManager<ApplicationUser> userManager, IShelterRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
            _userManager.KeyNormalizer = new UpperInvariantLookupNormalizer();
        }

         public async Task<IEnumerable<Shelter>> GetAllAsync()
        {
            var shelters = _repository.GetAll();

            return shelters;
        }

        public async Task<Shelter> GetByIdAsync(Guid id)
        {
            var shelter = _repository.Get(id);
            if (shelter == null)
            {
                throw new Exception("User not found");
            }
            return shelter;
        }

        public async Task<Shelter> CreateAsync(CreateShelterRequest dto)
        {
            var normalizedEmail = _userManager.NormalizeEmail(dto.Email);
            var shelter = new Shelter
            {
                UserName = dto.Email,
                Email = dto.Email,
                Address = dto.Address,
                Name = dto.Name,
                Capacity = dto.capacity,
                Website = dto.Website,
                isNoKill = dto.isNoKill,
                PhoneNumber = dto.PhoneNumber,
                NormalizedEmail = normalizedEmail,
            };
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var passwordCheck = await passwordValidator.ValidateAsync(_userManager, shelter, dto.Password);

            if (!passwordCheck.Succeeded)
            {
                throw new Exception("Password does not meet requirements");
            }

            var result = await _userManager.CreateAsync(shelter, dto.Password);


            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user");
            }
            var roleResult = await _userManager.AddToRoleAsync(shelter, UserRole.User);
            if(!roleResult.Succeeded)
            {
                throw new Exception("Failed to add user to role");
            }

            return shelter;
        }

        public async Task<Shelter> UpdateAsync(Guid id, UpdateShelterRequest dto)
        {
            var shelter = _repository.Get(id);
            if (shelter == null)
            {
                throw new Exception("Adopter not found");
            }

            shelter.Name = dto.Name;
            shelter.Capacity = dto.capacity;
            shelter.isNoKill = dto.isNoKill;
            shelter.PhoneNumber = dto.PhoneNumber;
            shelter.Website = dto.Website;

            _repository.Update(shelter);

            return shelter;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var shelter = _repository.Get(id);

            if (shelter == null)
            {
                return false;
            }
            var result =  await _userManager.DeleteAsync(shelter);

            if(result == null) {
                return false;
            }
            _repository.Delete(shelter);

            return true;
           
        }
    }
}