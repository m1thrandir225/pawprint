using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain;
using Domain.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.Config;

namespace Web.Services;

public class JWTService
{
    private readonly JWTConfig _jwtConfig;
    private readonly UserManager<ApplicationUser> _userManager;

    public JWTService(IOptions<JWTConfig> jwtSettings, UserManager<ApplicationUser> userManager)
    {
        _jwtConfig = jwtSettings.Value;
        _userManager = userManager;
    }

    public async Task<string> GenerateTokenAsync(ApplicationUser user, bool isAcessToken = true)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        // Add roles to claimss
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        if(isAcessToken) {
            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.ExpireMinutes),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        } else {
            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwtConfig.RefreshTokenExpireDays),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

    public DateTime GetExpirationTime(string token) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if(securityToken == null) {
            throw new Exception("Invalid token");
        }
        return securityToken.ValidTo;
    }
    public ClaimsPrincipal? VerifyToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtConfig.Issuer,
                ValidAudience = _jwtConfig.Audience,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero // Optional: Adjust for clock drift
            };

            var principal = tokenHandler.ValidateToken(token, parameters, out _);
            return principal;
        }
        catch
        {
            return null; // Token is invalid
        }
    }

    public string GenerateAccessTokenFromRefreshToken(string refreshToken, string userEmail, Guid userId) {
        var principal = VerifyToken(refreshToken);
        if (principal == null) {
            throw new Exception("Invalid refresh token");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.SecretKey);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, userEmail),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtConfig.ExpireMinutes),
            signingCredentials:  new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        );

        return tokenHandler.WriteToken(token);
    }


}