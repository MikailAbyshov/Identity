using Application.Interacting;
using Application.Operating;
using Application.Utils;
using Domain.ValueObjects;

namespace Application.Services.Users;

internal sealed class UserService : IUserService
{
  private readonly IUserRepository userRepository;
  
  public UserService(IUserRepository userRepository)
  {
    this.userRepository = userRepository;
  }
  
  public async Task<bool> Authorize(
    string name,
    string password,
    CancellationToken cancellationToken)
  {
    var user = await userRepository.GetByAuthData(name, new Password(password), cancellationToken);

    return user is not null;
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