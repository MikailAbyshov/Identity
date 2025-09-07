using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Shared.Utils;

/// <summary>
/// Статический класс-помощник для нуллябельных типов
/// </summary>
public static class RequiredExtensions
{
  /// <summary>
  /// Преобразует nullable тип в non-nullable, выбрасывая исключение если значение null
  /// </summary>
  public static T Required<T>([NotNull] this T? value, [CallerArgumentExpression("value")] string? paramName = null)
      where T : class
  {
    if (value is null)
    {
      throw new ArgumentNullException(paramName, $"Parameter '{paramName}' cannot be null");
    }

    return value;
  }

  /// <summary>
  /// Преобразует nullable value type в non-nullable, выбрасывая исключение если значение null
  /// </summary>
  public static T Required<T>([NotNull] this T? value, [CallerArgumentExpression("value")] string? paramName = null)
      where T : struct
  {
    if (value is null)
    {
      throw new ArgumentNullException(paramName, $"Parameter '{paramName}' cannot be null");
    }

    return value.Value;
  }

  /// <summary>
  /// Преобразует nullable строку в non-nullable, проверяя также на пустую строку
  /// </summary>
  public static string Required([NotNull] this string? value, [CallerArgumentExpression("value")] string? paramName = null)
  {
    if (string.IsNullOrWhiteSpace(value))
    {
      throw new ArgumentNullException(paramName, $"Parameter '{paramName}' cannot be null or empty");
    }

    return value;
  }
}