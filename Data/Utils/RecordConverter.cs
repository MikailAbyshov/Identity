using Data.Records;
using Domain.Entities;
using Shared.Utils;

namespace Data.Utils;

internal static class RecordConverter
{
  public static User ToUser(this UserRecord userRecord)
  {
    var id = userRecord.Id.Required();
    var passwordHash = userRecord.PasswordHash.Required();
    var passwordSalt = userRecord.PasswordSalt.Required();
    var externalId = userRecord.ExternalId.Required();
    var name = userRecord.Name.Required();
    var createdBy = userRecord.CreatedBy.Required();
    var createdAt = userRecord.CreatedAt.Required();
    var updatedAt = userRecord.UpdatedAt.Required();

    return User.Create(
      id,
      passwordHash,
      passwordSalt,
      externalId,
      name,
      createdBy,
      createdAt,
      updatedAt,
      userRecord.DeletedAt);
  }

  public static UserRecord ToRecord(this User user)
  {
    return new UserRecord
    {
      PasswordHash = user.Password.Hash,
      PasswordSalt = user.Password.Salt,
      ExternalId = user.ExternalId,
      Id = user.Id,
      Name = user.Name,
      CreatedAt = user.CreatedAt,
      UpdatedAt = user.UpdatedAt,
      DeletedAt = user.DeletedAt,
      CreatedBy = user.CreatedBy
    };
  }
}