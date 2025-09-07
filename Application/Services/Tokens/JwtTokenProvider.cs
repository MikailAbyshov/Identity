using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Services.Tokens;

internal sealed class JwtTokenProvider : IJwtTokenProvider
{
  private readonly JwtOptions options;

  public JwtTokenProvider(IOptions<JwtOptions> options)
  {
    this.options = options.Value;
  }

  public string GenerateToken()
  {
    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
      SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      signingCredentials: signingCredentials,
      expires: DateTime.UtcNow.Add(options.TokenLifetime));

    var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

    return tokenValue;
  }
}