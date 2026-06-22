using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.OrderAgent.Api.Security;
using JacksonVeroneze.OrderAgent.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JacksonVeroneze.OrderAgent.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                options.Authority = appConfiguration.Auth!.Authority;
                options.Audience = appConfiguration.Auth.Audience;

                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appConfiguration!.Auth!.Issuer,
                        ValidAudience = appConfiguration.Auth.Audience,
                        ClockSkew = TimeSpan.Zero,
                    };
            })
            .AddApiKeyAuthenticationScheme(configuration);

        return services;
    }
}
