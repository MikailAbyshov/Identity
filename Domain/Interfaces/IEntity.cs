namespace Domain.Interfaces;

/// <summary>
/// Маркерный интерфейс сущности
/// </summary>
public interface IEntity
{
  /// <summary>
  /// Идентификатор сущности
  /// </summary>
  public Guid Id { get; }
}