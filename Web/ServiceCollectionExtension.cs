using Application.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web;

/// <summary>
/// Добавление аутентификации в DI
/// </summary>
public static class ServiceCollectionExtension
{
  public static void AddApiAuthentification(
    this IServiceCollection services,
    IConfiguration configuration)
  {
    var jwtOptions = configuration.GetSection(JwtOptions.OptionsKey).Get<JwtOptions>();

    if (jwtOptions is null)
    {
      throw new ArgumentNullException();
    }

    services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
        };

        options.Events = new JwtBearerEvents
        {
          OnMessageReceived = context =>
          {
            context.Token = context.Request.Cookies["niceCookie"];

            return Task.CompletedTask;
          }
        };
      });

    services.AddAuthorization();
  }
}