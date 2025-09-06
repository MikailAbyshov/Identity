namespace Domain.Interfaces;

/// <summary>
/// Сущности, создаваемые во внешних сервисах
/// </summary>
public interface IExternalEntity
{
    /// <summary>
    /// Идентификатор сущности из внешнего сервиса
    /// </summary>
    string ExternalId { get; }
}