using Domain.Interfaces;

namespace Application.Services.Cache;

/// <summary>
/// Сервис работы с распределённым кэшем
/// </summary>
public interface IUserCacheService
{
  /// <summary>
  /// Получить данные из кэша
  /// </summary>
  Task<bool?> Get(
    string password,
    string name,
    CancellationToken cancellationToken);

  /// <summary>
  /// Поместить данные в кэш
  /// </summary>
  Task Set(
    string password,
    string name,
    bool value,
    CancellationToken cancellationToken);

  /// <summary>
  /// Удалить данные из кэша
  /// </summary>
  Task Delete(
    string password,
    string name,
    CancellationToken cancellationToken);
}