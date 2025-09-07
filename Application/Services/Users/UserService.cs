using Application.Interacting;
using Application.Operating;
using Application.Services.Cache;
using Application.Utils;
using Shared.Utils;

namespace Application.Services.Users;

internal sealed class UserService : IUserService
{
  private readonly IUserRepository userRepository;
  private readonly IUserCacheService userCacheService;
  
  public UserService(
    IUserRepository userRepository,
    IUserCacheService userCacheService)
  {
    this.userRepository = userRepository;
    this.userCacheService = userCacheService;
  }
  
  public async Task<bool> Authorize(
    string? name,
    string? password,
    CancellationToken cancellationToken)
  {
    var authName = name.Required();
    var authPassword = password.Required();

    var cachedValue = await userCacheService.Get(password, name, cancellationToken);

    if (cachedValue is not null)
    {
      return cachedValue.Value;
    }
    
    var userPassword = await userRepository.GetByName(authName, cancellationToken);

    if (userPassword is null || !userPassword.Verify(authPassword))
    {
      return false;
    }

    await userCacheService.Set(
      password,
      name,
      true,
      cancellationToken);

    return true;
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