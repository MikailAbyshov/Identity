using Application.Interacting;
using Application.Operating;
using Application.Utils;
using Shared.Utils;

namespace Application.Services.Users;

internal sealed class UserService : IUserService
{
  private readonly IUserRepository userRepository;
  
  public UserService(IUserRepository userRepository)
  {
    this.userRepository = userRepository;
  }
  
  public async Task<bool> Authorize(
    string? name,
    string? password,
    CancellationToken cancellationToken)
  {
    var authName = name.Required();
    var authPassword = password.Required();
    
    var userPassword = await userRepository.GetByName(authName, cancellationToken);

    return userPassword is not null && userPassword.Verify(authPassword);
  }

  public async Task<Guid> Create(
    UserDto userDto,
    CancellationToken cancellationToken)
  {
    var user = userDto.ConvertToUser();
    
    await userRepository.Create(user, cancellationToken);

    return user.Id;
  }
}