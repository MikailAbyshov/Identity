using Application.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Web.Operating.Requests;
using Web.Operating.Requests.Users;

namespace Web.Controllers.Users;

/// <summary>
/// Контроллер пользователей
/// </summary>
[ApiController]
[Route("api/boxes")]
public sealed class BoxesController(IUserService userService) : ControllerBase
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
}