using Application.Interacting;
using Data.Engine;
using Data.Utils;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Shared.Utils;

namespace Data.Repositories;

internal sealed class UserRepository : IUserRepository
{
  private readonly IIdentityContext context;
  
  public UserRepository(IIdentityContext context)
  {
    this.context = context;
  }
  
  public async Task<Password?> GetByName(
    string name,
    CancellationToken cancellationToken)
  {
    var authName = name.Required();
    
    var userRecord = await context.Users
      .SingleOrDefaultAsync(u => 
          u.Name == authName
          && u.DeletedAt == null, 
        cancellationToken);

    if (userRecord is not null)
    {
      userRecord.PasswordHash.Required();
      userRecord.PasswordSalt.Required();
      
      return new Password(userRecord.PasswordHash, userRecord.PasswordSalt);
    }
    
    return null;
  }

  public Task Create(User user, CancellationToken cancellationToken)
  {
    var record = user.ToRecord();
    
    context.Users.AddAsync(record, cancellationToken);

    return context.SaveChangesAsync(cancellationToken);
  }
}