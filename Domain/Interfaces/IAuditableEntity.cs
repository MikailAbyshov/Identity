namespace Domain.Interfaces;

/// <summary>
/// Сущность с аудитом
/// </summary>
public interface IAuditableEntity : IEntity
{
  /// <summary>
  /// Дата создания
  /// </summary>
  DateTimeOffset CreatedAt { get; }

  /// <summary>
  /// Дата обновления
  /// </summary>
  DateTimeOffset UpdatedAt { get; }

  /// <summary>
  /// Дата/признак удаления
  /// </summary>
  DateTimeOffset? DeletedAt { get; }

  /// <summary>
  /// Создатель
  /// </summary>
  string CreatedBy { get; }
}