using Application.Services.Tokens;
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
  [ProducesResponseType<string>(StatusCodes.Status200OK)]
  public IActionResult Login()
  {
    var token = tokenProvider.GenerateToken();
   
    Response.Cookies.Append("niceCookie", token);
    
    return Ok(token);
  }
}