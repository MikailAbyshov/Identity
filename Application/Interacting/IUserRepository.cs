using Domain.Entities;

namespace Application.Interacting;

/// <summary>
/// Репозиторий пользователей серивса
/// </summary>
public interface IUserRepository
{
  /// <summary>
  /// Получить пользователя по данным авторизации
  /// </summary>
  public Task<User> GetByAuthData(string name, string password);
}