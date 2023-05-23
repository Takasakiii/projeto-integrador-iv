using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobsApi.Configs;
using JobsApi.Dtos;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Microsoft.IdentityModel.Tokens;

namespace JobsApi.Services;

[Service(typeof(IJwtService))]
public class JwtService : IJwtService
{
    private readonly IAppConfig _appConfig;

    public JwtService(IAppConfig appConfig)
    {
        _appConfig = appConfig;
    }
    
    private string GenerateToken(IEnumerable<Claim> claims, TimeSpan expireTimeSpan)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = Encoding.UTF8.GetBytes(_appConfig.Jwt.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow + expireTimeSpan,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateUserToken(UserDto user)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.UInteger32),
            new Claim(ClaimTypes.Role, user.Type.ToString())
        };

        var expire = TimeSpan.FromDays(7);
        return GenerateToken(claims, expire);
    }
}