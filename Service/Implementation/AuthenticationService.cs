using Domain.DTOs;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Service.Implementation
{
    public class AuthenticationService 
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager) 
        {
            _userManager = userManager;
            _userManager.KeyNormalizer = new UpperInvariantLookupNormalizer();
        }

        public async Task<ApplicationUser> LoginAsync(LoginRequest request)
        {
            var normalizedEmail = _userManager.NormalizeEmail(request.Email);
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                var userCount = await _userManager.Users.CountAsync();
                throw new Exception("Invalid password");
            }

            return user;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
        
    }
}