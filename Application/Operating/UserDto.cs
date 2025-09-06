namespace Application.Operating;

public sealed record UserDto(
  string Password,
  string ExternalId,
  string Name,
  string CreatedBy,
  DateTimeOffset CreatedAt);