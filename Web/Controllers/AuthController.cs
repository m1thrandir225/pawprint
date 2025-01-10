using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Config;
using Web.DTOs;
using Web.Services;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTService _jwtService;

        public AuthController(UserManager<ApplicationUser> userManager, JWTService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return Unauthorized();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var accessToken = await _jwtService.GenerateTokenAsync(user);

            return Ok(new
                LoginResponse(accessToken, user)
            );
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<ActionResult<RefreshTokenResponse>> RefreshToken(
            [FromBody] RefreshTokenRequest refreshRequest)
        {
            var claims =  _jwtService.VerifyToken(refreshRequest.RefreshToken);

            if (claims == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByEmailAsync(refreshRequest.Email);
            if (user is null)
            {
                return Unauthorized();
            }

            var newAccessToken = await _jwtService.GenerateTokenAsync(user);

            return Ok(new RefreshTokenResponse(newAccessToken, refreshRequest.RefreshToken));
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<ActionResult<bool>> Logout()
        {
            //TODO: add verification
            return Ok();
        }
    }
}
