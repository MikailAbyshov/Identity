namespace Web.Operating.Requests.Users;

internal sealed class GetUserRequest
{
    public string? Name { get; set; }
    
    public string? Password { get; set; }
}