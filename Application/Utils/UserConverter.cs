using Application.Operating;
using Domain.Entities;

namespace Application.Utils;

internal static class UserConverter
{
  public static User ConvertToUser(this UserDto dto)
  {
    return User.Create(
      dto.Password,
      dto.ExternalId,
      dto.Name,
      dto.CreatedBy,
      dto.CreatedAt);
  }
}