using Application.Operating;
using Domain.Entities;

namespace Application.Services.Users;

/// <summary>
/// Сервис пользователей
/// </summary>
public interface IUserService
{
  /// <summary>
  /// Авторизовать пользователя
  /// </summary>
  Task<bool> Authorize(string name, string password, CancellationToken cancellationToken);

  /// <summary>
  /// Создать пользователя
  /// </summary>
  Task<Guid> Create(UserDto userDto, CancellationToken cancellationToken);
}