using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Web.Services.Interfaces;

namespace Web.Services;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
     var httpContext = _httpContextAccessor.HttpContext;

     var userId = httpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

     if (userId == null)
     {
         throw new UnauthorizedAccessException("User is not authenticated");
     }
     return Guid.Parse(userId);
    }

}