namespace Application.Services.Tokens;

public sealed class JwtOptions
{
  public static string OptionsKey { get; } = "Jwt";

  public string SigningKey { get; set; } = " ";

  public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromHours(12);
}