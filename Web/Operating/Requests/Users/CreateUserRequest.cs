namespace Web.Operating.Requests.Users;

public sealed class CreateUserRequest
{
  public string? Password { get; set; }

  public string? ExternalId { get; set; }

  public string? Name { get; set; }

  public string? CreatedBy { get; set; }
}