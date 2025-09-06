using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Interacting;

/// <summary>
/// Репозиторий пользователей серивса
/// </summary>
public interface IUserRepository
{
  /// <summary>
  /// Получить пользователя по данным авторизации
  /// </summary>
  Task<User?> GetByAuthData(
    string name,
    Password password,
    CancellationToken cancellationToken);

  /// <summary>
  /// Получить пользователя по данным авторизации
  /// </summary>
  Task Create(
    User user,
    CancellationToken cancellationToken);
}