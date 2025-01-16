using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;
using Web.Services;

namespace Web.Controllers {
    [Route("/api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAdopterService _adopterService;
        private readonly AuthenticationService _authenticationService;
        private readonly IShelterService _shelterService;
        private readonly JWTService _jwtService;

        public AuthenticationController(
            IAdopterService adopterService, 
            IShelterService shelterService,
            AuthenticationService authenticationService,
            JWTService jwtService
        )
        {
            _authenticationService = authenticationService;
            _adopterService = adopterService;
            _shelterService = shelterService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            try {
                var user = await _authenticationService.LoginAsync(request);

                var accessToken = await _jwtService.GenerateTokenAsync(user);
                var refreshToken = await _jwtService.GenerateTokenAsync(user, false);
                if (user is Shelter)
                {
                    LoginShelterResponse response = new LoginShelterResponse();
                    var shelter = await _shelterService.GetByIdAsync(user.Id);
                    var details = new ShelterDTO {
                        Address = user.Address,
                        Email = user.Email,
                        Name = shelter.Name,
                        PhoneNumber = shelter.PhoneNumber,
                        Website = shelter.Website,
                    };
                    response.Shelter = details;
                    response.AccessToken = accessToken;
                    response.RefreshToken = refreshToken;

                    return Ok(response);
                } else if (user is User) 
                {
                    LoginUserResponse response = new LoginUserResponse();
                    var adopter = await _adopterService.GetByIdAsync(user.Id);
                    
                    var details = new UserDTO {
                        Address = user.Address,
                        Email = user.Email,
                        FirstName = adopter.FirstName,
                        LastName = adopter.LastName,
                        HasChildren = adopter.HasChildren,
                        HasOtherPets = adopter.HasOtherPets,
                        HomeType = adopter.HomeType,
                    };
                    
                    response.User = details;
                    response.AccessToken = accessToken;
                    response.RefreshToken = refreshToken;

                    return Ok(response);

                } else {
                    return BadRequest();
                }
            } catch (Exception e) {
                if (e.Message == "User not found"){
                    return NotFound("User not found");
                } else if (e.Message == "Invalid password") {
                    return Unauthorized("Invalid password");
                } else {
                    return BadRequest("An error occurred");
                }
            }

       
        }

        [HttpPost("register/adopter")]
        public async Task<ActionResult<UserDTO>> RegisterAdopter([FromBody] CreateAdopterRequest request)
        {
            try {
            var adopter = await _adopterService.CreateAsync(request);
            var response = new UserDTO {
                Address = adopter.Address,
                Email = adopter.Email,
                FirstName = adopter.FirstName,
                LastName = adopter.LastName,
                HasChildren = adopter.HasChildren,
                HasOtherPets = adopter.HasOtherPets,
                HomeType = adopter.HomeType,
            };
            return Ok(response);

            } catch (Exception e) 
            {
                if (e.Message == "Password does not meet requirements") {
                    return BadRequest("Password does not meet requirements");
                } else if (e.Message == "Failed to create user") {
                    return BadRequest("Failed to create user");
                } else {
                    return BadRequest("An error occurred");
                }
            }

        }
        [HttpPost("register/shelter")]
        public async Task<ActionResult<ShelterDTO>> RegisterShelter([FromBody] CreateShelterRequest request)
        {
            try {
                var shelter = await _shelterService.CreateAsync(request);

                var response = new ShelterDTO {
                    Address = shelter.Address,
                    Email = shelter.Email,
                    Name = shelter.Name,
                    PhoneNumber = shelter.PhoneNumber,
                    Website = shelter.Website,
                    Capacity = shelter.Capacity,
                    isNoKill = shelter.isNoKill,
                };

                return Ok(response);
            } catch (Exception e) {
                if (e.Message == "Password does not meet requirements") {
                    return BadRequest("Password does not meet requirements");
                } else if (e.Message == "Failed to create user") {
                    return BadRequest("Failed to create user");
                } else {
                    return BadRequest("An error occurred");
                }
            }
            
        }
        [HttpPost("refresh")]
        public  ActionResult<string> GenerateRefreshToken([FromBody] RefreshTokenRequest request)
        {
            try {
                var token = _jwtService.GenerateAccessTokenFromRefreshToken(request.RefreshToken, request.Email);
                var response = new RefreshTokenResponse {
                    AccessToken = token
                };
                return Ok(response);

            } catch(Exception e) {
                if (e.Message == "Invalid refresh token") {
                    return Unauthorized("Invalid refresh token");
                } else {
                    return Unauthorized();
                }
            }

        }
    }
}