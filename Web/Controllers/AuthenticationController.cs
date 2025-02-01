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
    [AllowAnonymous] 
    public class AuthenticationController : ControllerBase
    {
        private readonly IAdopterService _adopterService;
        private readonly AuthenticationService _authenticationService;
        private readonly IShelterService _shelterService;
        private readonly JWTService _jwtService;
        private readonly IEmailService _emailService;


        public AuthenticationController(
            IAdopterService adopterService, 
            IShelterService shelterService,
            AuthenticationService authenticationService,
            JWTService jwtService,
            IEmailService emailService
        )
        {
            _authenticationService = authenticationService;
            _adopterService = adopterService;
            _shelterService = shelterService;
            _jwtService = jwtService;
            _emailService = emailService;
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
                        Id = shelter.Id,
                        Address = user.Address,
                        Email = user.Email,
                        Name = shelter.Name,
                        PhoneNumber = shelter.PhoneNumber,
                        Website = shelter.Website,
                        Capacity = shelter.Capacity,
                        isNoKill = shelter.isNoKill,
                    };
                    response.Shelter = details;
                    response.AccessToken = accessToken;
                    response.RefreshToken = refreshToken;
                    response.AccessTokenExpirationTime = _jwtService.GetExpirationTime(accessToken);
                    response.RefreshTokenExpirationTime = _jwtService.GetExpirationTime(refreshToken);
                    

                    return Ok(response);
                } else if (user is User) 
                {
                    LoginUserResponse response = new LoginUserResponse();
                    var adopter = await _adopterService.GetByIdAsync(user.Id);
                    
                    var details = new UserDTO {
                        Id = user.Id,
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
                    response.AccessTokenExpirationTime = _jwtService.GetExpirationTime(accessToken);
                    response.RefreshTokenExpirationTime = _jwtService.GetExpirationTime(refreshToken);

                    return Ok(response);

                } else {
                    return BadRequest(new ErrorResponse {
                        Message = "An error occurred"
                    });
                }
            } catch (Exception e) {
                if (e.Message == "User not found"){
                    return NotFound(new ErrorResponse{
                        Message = "User not found"
                    });
                } else if (e.Message == "Invalid password") {
                    return Unauthorized(new ErrorResponse {
                        Message = "Invalid password"
                    });
                } else {
                    return BadRequest(new ErrorResponse {
                        Message = "An error occurred"
                    });
                }
            }

       
        }

        [HttpPost("register/adopter")]
        public async Task<ActionResult<UserDTO>> RegisterAdopter([FromBody] CreateAdopterRequest request)
        {
            try
            {
                var adopter = await _adopterService.CreateAsync(request);
                var response = new UserDTO {
                    Id = adopter.Id,
                    Address = adopter.Address,
                    Email = adopter.Email,
                    FirstName = adopter.FirstName,
                    LastName = adopter.LastName,
                    HasChildren = adopter.HasChildren,
                    HasOtherPets = adopter.HasOtherPets,
                    HomeType = adopter.HomeType,

                };
                var emailSent = await _emailService.SendRegistrationConfirmationAsync(adopter.Email, UserType.Adopter);
                if (!emailSent)
                {
                    throw new Exception("Email could not be sent");
                }

                return Ok(response);

            }
            catch (Exception e)
            {
                return BadRequest(new ErrorResponse {
                    Message = e.Message
                });
            }

        }
        [HttpPost("register/shelter")]
        public async Task<ActionResult<ShelterDTO>> RegisterShelter([FromBody] CreateShelterRequest request)
        {
            try {
                var shelter = await _shelterService.CreateAsync(request);

                var response = new ShelterDTO {
                    Id = shelter.Id,
                    Address = shelter.Address,
                    Email = shelter.Email,
                    Name = shelter.Name,
                    PhoneNumber = shelter.PhoneNumber,
                    Website = shelter.Website,
                    Capacity = shelter.Capacity,
                    isNoKill = shelter.isNoKill,

                };

                var emailSent = await _emailService.SendRegistrationConfirmationAsync(shelter.Email, UserType.Shelter);
                if (!emailSent)
                {
                    throw new Exception("Email could not be sent");
                }

                return Ok(response);

                return Ok(response);
            } catch (Exception e) {
               return BadRequest(new ErrorResponse {
                   Message = e.Message
               });
            }
            
        }
        [HttpPost("refresh")]
        public  ActionResult<string> GenerateRefreshToken([FromBody] RefreshTokenRequest request)
        {
            try {
                var token = _jwtService.GenerateAccessTokenFromRefreshToken(request.RefreshToken, request.Email);
                var refreshTime = _jwtService.GetExpirationTime(token);
                var response = new RefreshTokenResponse {
                    AccessToken = token,
                    AccessTokenExpirationTime = refreshTime
                };
                return Ok(response);

            } catch(Exception e) {
                return BadRequest(new ErrorResponse {
                    Message = e.Message
                });
            }

        }
    }
}