using System.Text;
using JobsApi.Configs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JobsApi.Services;

public class JwtConfigureService : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IAppConfig _appConfig;

    public JwtConfigureService(IAppConfig appConfig)
    {
        _appConfig = appConfig;
    }

    public void Configure(string name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme) return;
        
        var secret = Encoding.UTF8.GetBytes(_appConfig.Jwt.Secret);

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secret),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }

    public void Configure(JwtBearerOptions options)
    {
        Configure(string.Empty, options);
    }
}