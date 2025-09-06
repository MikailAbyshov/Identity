using Application.Operating;
using Web.Operating.Requests.Users;

namespace Web.Operating.Requests;

internal static class UserConverter
{
  public static UserDto ConvertToUserDto(this CreateUserRequest request, DateTimeOffset createdAt)
  {
    if (request is { ExternalId: not null, Name: not null, CreatedBy: not null, Password: not null})
    {
      return new UserDto(request.Password, request.ExternalId, request.Name, request.CreatedBy, createdAt);
    }

    throw new ArgumentException();
  } 
}