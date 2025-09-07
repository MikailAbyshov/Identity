using Application.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Operating.Requests;
using Web.Operating.Requests.Users;

namespace Web.Controllers.Users;

/// <summary>
/// Контроллер пользователей
/// </summary>
[ApiController]
[Authorize]
[Route("api/users")]
public sealed class UserController(IUserService userService) : ControllerBase
{
  /// <summary>
  /// Создать пользователя
  /// </summary>
  [HttpPost(Name = "CreateUser")]
  [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Create(
    [FromBody] CreateUserRequest userRequest,
    CancellationToken cancellationToken)
  {
    var userDto = userRequest.ConvertToUserDto(DateTimeOffset.Now);

    await userService.Create(userDto, cancellationToken);

    return Ok(userDto.ExternalId);
  }

  /// <summary>
  /// Авторизовать пользователя
  /// </summary>
  [HttpGet(Name = "Authorize")]
  [ProducesResponseType<bool>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Create(
    [FromQuery] LoginRequest request,
    CancellationToken cancellationToken)
  {
    if (await userService.Authorize(request.Username, request.Password, cancellationToken))
    {
      return Ok();
    }

    return NotFound();
  }
}