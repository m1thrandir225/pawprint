using Domain;
using Domain.DTOs;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation
{
    public class AdopterService : IAdopterService
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdopterService(UserManager<ApplicationUser> userManager, IUserRepository repository) 
        {
            _userManager = userManager;
            _repository = repository;
            _userManager.KeyNormalizer = new UpperInvariantLookupNormalizer();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var adopters = _repository.GetAll();

            return adopters;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var adopter =  _repository.Get(id);
            if (adopter == null)
            {
                throw new Exception("User not found");
            }
            return adopter;
        }

        public async Task<User> CreateAsync(CreateAdopterRequest dto)
        {
            var normalizedEmail = _userManager.NormalizeEmail(dto.Email);
            var adopter = new User
            {
                UserName = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                HasChildren = dto.HasChildren,
                HasOtherPets = dto.HasOtherPets,
                Address = dto.Address,
                HomeType = dto.HomeType,
                Email = dto.Email,
                NormalizedEmail = normalizedEmail,
            };
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var passwordCheck = await passwordValidator.ValidateAsync(_userManager, adopter, dto.Password);

            if(!passwordCheck.Succeeded)
            {
                throw new Exception("Password does not meet requirements");
            }

            var result = await _userManager.CreateAsync(adopter, dto.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user");
            }

            var roleResult = await _userManager.AddToRoleAsync(adopter, UserRole.User);
            if(!roleResult.Succeeded)
            {
                throw new Exception("Failed to add user to role");
            }

            return adopter;
        }

        public async Task<User> UpdateAsync(Guid id, UpdateAdopterRequest dto)
        {
            var adopter = _repository.Get(id);
            if (adopter == null)
            {
                throw new Exception("Adopter not found");
            }

            adopter.FirstName = dto.FirstName;
            adopter.LastName = dto.LastName;
            adopter.HasChildren = dto.HasChildren;
            adopter.HasOtherPets = dto.HasOtherPets;
            adopter.HomeType = dto.HomeType;

            _repository.Update(adopter);

            return adopter;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var adopter = _repository.Get(id);

            if (adopter == null)
            {
                return false;
            }
            var result =  await _userManager.DeleteAsync(adopter);

            if(result == null) {
                return false;
            }
            _repository.Delete(adopter);

            return true;
           
        }
    }
}