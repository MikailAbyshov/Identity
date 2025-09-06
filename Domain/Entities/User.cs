using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Пользователь сервиса
/// </summary>
public sealed class User : 
    IExternalEntity, 
    IAuditableEntity
{
    public Password Password { get; private set; }
    
    /// <inheritdoc/>
    public string ExternalId { get; private set; }
    
    /// <summary>
    /// Идентификатор в сервисе Identity
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; private set; }
    
    /// <inheritdoc/>
    public DateTimeOffset CreatedAt { get; private set; }
    
    /// <inheritdoc/>
    public DateTimeOffset UpdatedAt { get; private set; }
    
    /// <inheritdoc/>
    public DateTimeOffset? DeletedAt { get; private set; }
    
    /// <inheritdoc/>
    public string CreatedBy { get; private set; }
    
    private User(
        Password password,
        string externalId,
        string name,
        string createdBy,
        DateTimeOffset createdAt,
        DateTimeOffset updatedAt)
    {
        Password = password;
        ExternalId = externalId;
        Id = Guid.NewGuid();
        Name = name;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    /// <summary>
    /// Создать нового пользователя
    /// </summary>
    public static User Create(
        string password,
        string externalId,
        string name,
        string createdBy,
        DateTimeOffset createdAt)
    {
        var identityPassword = new Password(password);

        return new User(
            identityPassword,
            externalId,
            name,
            createdBy,
            createdAt,
            createdAt);
    }
}