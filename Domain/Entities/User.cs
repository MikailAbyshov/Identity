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
    Guid id,
    Password password,
    string externalId,
    string name,
    string createdBy,
    DateTimeOffset createdAt,
    DateTimeOffset updatedAt,
    DateTimeOffset? deletedAt)
  {
    Password = password;
    ExternalId = externalId;
    Id = id;
    Name = name;
    CreatedBy = createdBy;
    CreatedAt = createdAt;
    UpdatedAt = updatedAt;
    DeletedAt = deletedAt;
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
      Guid.NewGuid(),
      identityPassword,
      externalId,
      name,
      createdBy,
      createdAt,
      createdAt,
      null);
  }

  public static User Create(
    Guid id,
    string passwordHash,
    string passwordSalt,
    string externalId,
    string name,
    string createdBy,
    DateTimeOffset createdAt,
    DateTimeOffset updatedAt,
    DateTimeOffset? deletedAt)
  {
    var identityPassword = new Password(passwordHash, passwordSalt);

    return new User(
      id,
      identityPassword,
      externalId,
      name,
      createdBy,
      createdAt,
      updatedAt,
      deletedAt);
  }
}