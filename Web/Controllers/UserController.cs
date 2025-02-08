using System.Net;
using Domain;
using Domain.DTOs;
using Domain.DTOs.Identity;
using Domain.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Web.Controllers;

[Route("api/users")]
[ApiController]
[Authorize(Roles = $"{UserRole.Admin}")]
public class UserController
{
    private readonly IShelterService _shelterService;
    private readonly IAdopterService _adopterService;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(
        IShelterService shelterService,
        IAdopterService adopterService,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager
        )
    {
        _shelterService = shelterService;
        _adopterService = adopterService;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    [HttpGet]
    [Route("shelters")]
    public async Task<IEnumerable<ShelterDTO>> GetSheltersAsync()
    {
        var shelters = await _shelterService.GetAllAsync();
        var sheltersDto = new List<ShelterDTO>();

        sheltersDto.AddRange(shelters.Select(shelter => new ShelterDTO
        {
            Id = shelter.Id,
            Name = shelter.Name,
            Email = shelter.Email,
            PhoneNumber = shelter.PhoneNumber,
            Address = shelter.Address,
            Capacity = shelter.Capacity,
            Website = shelter.Website,
            isNoKill = shelter.isNoKill,
        }));
        return sheltersDto;
    }
    [HttpGet]
    [Route("adopters")]
    public async Task<IEnumerable<UserDTO>> GetUsersAsync()
    {
        var users = await _adopterService.GetAllAsync();
        var usersDto = new List<UserDTO>();

        usersDto.AddRange(users.Select(user => new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            HasChildren = user.HasChildren,
            HomeType = user.HomeType,
            HasOtherPets = user.HasOtherPets,
        }));

        return usersDto;
    }

    [HttpGet]
    [Route("roles")]
    public async Task<ICollection<object>> GetAllUserRolesAsync()
    {
        var roles = _roleManager.Roles.ToList();

        if (roles == null || !roles.Any())
        {
            return new List<object>();
        }
        var roleDto = new List<object>();
        foreach (var role in roles)
        {
            roleDto.Add(new
            {
                name = role.Name,
            });
        }

        return roleDto;
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<HttpStatusCode> DeleteUserAsync([FromRoute] Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return HttpStatusCode.NotFound;
        }

        var result = await _userManager.DeleteAsync(user);

        return result.Succeeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
    }

}