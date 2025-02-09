using System.Security.Claims;
using Domain.DTOs;
using Domain.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Web.Filters;

public class AuthorizeWithUserId<RequestDTO> : AuthorizeAttribute, IAsyncAuthorizationFilter
    where RequestDTO : class, IUserResource // Constraint to ensure RequestDTO has UserId
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // Extract userId from the JWT token
        var userIdString = context.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid tokenUserId))
        {
            context.Result = new UnauthorizedResult(); // 401 Unauthorized
            return;
        }

        var isAdmin = context.HttpContext.User.IsInRole(UserRole.Admin);
        if (isAdmin)
        {
            return;
        }

        // Read and deserialize the request body
        var request = await context.HttpContext.Request.ReadFromJsonAsync<RequestDTO>();
        if (request == null)
        {
            context.Result = new BadRequestObjectResult("Request body is invalid."); // 400 Bad Request
            return;
        }

        // Check if the UserId in the request matches the userId from the token
        if (request.UserId != tokenUserId)
        {
            context.Result = new ForbidResult(); // 403 Forbidden
            return;
        }

        // If everything is fine, the request will proceed to the action method
    }
}