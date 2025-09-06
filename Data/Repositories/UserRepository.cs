using Application.Interacting;
using Data.Engine;
using Data.Utils;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

internal sealed class UserRepository : IUserRepository
{
  private readonly IIdentityContext context;
  
  public UserRepository(IIdentityContext context)
  {
    this.context = context;
  }
  
  public async Task<User?> GetByAuthData(
    string name, 
    Password password,
    CancellationToken cancellationToken)
  {
    var record = await context.Users
      .Where(u => u.DeletedAt == null)
      .SingleOrDefaultAsync(u => 
          u.Name == name 
          && u.PasswordHash == password.Hash 
          && u.PasswordSalt == password.Salt, 
        cancellationToken);

    if (record is null)
    {
      throw new KeyNotFoundException();
    }
    
    return record.ToUser();
  }

  public Task Create(User user, CancellationToken cancellationToken)
  {
    var record = user.ToRecord();
    
    context.Users.AddAsync(record, cancellationToken);

    return context.SaveChangesAsync(cancellationToken);
  }
}