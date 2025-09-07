using Application.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Web;

/// <summary>
/// Добавление зависимостей в DI
/// </summary>
public static class ServiceCollectionExtension
{
  /// <summary>
  /// Добавление механизма аутентификации
  /// </summary>
  public static void AddApiAuthentification(
    this IServiceCollection services,
    IConfiguration configuration)
  {
    var jwtOptions = configuration.GetSection(JwtOptions.OptionsKey).Get<JwtOptions>();

    if (jwtOptions is null)
    {
      throw new ArgumentNullException(nameof(jwtOptions));
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
            var accessToken = context.Request.Headers["Authorization"]
              .FirstOrDefault();

            if (accessToken is null)
            {
              return Task.CompletedTask;
            }

            accessToken = accessToken.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

            if (!string.IsNullOrEmpty(accessToken))
            {
              context.Token = accessToken;
            }

            return Task.CompletedTask;
          }
        };
      });

    services.AddAuthorization();
  }

  /// <summary>
  /// Добавление Swagger с возможностью авторизации
  /// </summary>
  public static void AddSwaggerWithAuth(this IServiceCollection services)
  {
    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity API" });

      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Description = "Enter JWT token in format: {your_token}"
      });

      options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "Bearer"
            }
          },
          Array.Empty<string>()
        }
      });
    });
  }
}