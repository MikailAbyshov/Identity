namespace Data.Records;

public sealed class UserRecord
{
  public string? PasswordHash { get; set; }
  
  public string? PasswordSalt{ get; set; }
  
  public string? ExternalId { get; set; }
  
  public Guid? Id { get; set; }
  
  public string? Name { get; set; }
  
  public DateTimeOffset? CreatedAt { get; set; }

  public DateTimeOffset? UpdatedAt { get; set; }
  
  public DateTimeOffset? DeletedAt { get; set; }
  
  public string? CreatedBy { get; set; }
}