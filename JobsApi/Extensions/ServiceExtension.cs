using Config.Net;
using JobsApi.Configs;
using JobsApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace JobsApi.Extensions;

public static class ServiceExtension
{
    public static void AddAppConfig(this IServiceCollection serviceCollection)
    {
        var config = new ConfigurationBuilder<IAppConfig>()
            .UseEnvironmentVariables()
            .UseJsonFile("config.json")
            .Build();

        serviceCollection.AddSingleton(config);
    }

    public static void AddAppJwt(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
        serviceCollection.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtConfigureService>();
    }
    
    public static void AddAppSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(x =>
        {
            x.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
            });

            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}