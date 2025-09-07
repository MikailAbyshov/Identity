using Application.Services.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Clients;

/// <summary>
/// Контроллер потребителей сервиса
/// </summary>
[ApiController]
[Route("api/clients")]
public sealed class UserController(IJwtTokenProvider tokenProvider) : ControllerBase
{
  /// <summary>
  /// Аутентифицировать потребителя сервиса
  /// </summary>
  [HttpPost(Name = "Login")]
  [AllowAnonymous]
  [ProducesResponseType<string>(StatusCodes.Status200OK)]
  public IActionResult Login()
  {
    var token = tokenProvider.GenerateToken();

    return Ok(new { access_token = token, token_type = "Bearer" });
  }
}