using Domain.Entities;

namespace Application.Services.Tokens;

/// <summary>
/// Поставщик JWT-токенов
/// </summary>
public interface IJwtTokenProvider
{
  /// <summary>
  /// Сгенерировать токен
  /// </summary>
  string GenerateToken();
}