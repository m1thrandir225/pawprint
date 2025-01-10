using Domain.Identity;

namespace Web.DTOs;

public class LoginResponse
{
    public ApplicationUser User { get; set; }
    public string AccessToken { get; set; }

    public LoginResponse(string accessToken, ApplicationUser user)
    {
        AccessToken = accessToken;
        User = user;
    }
}