using Application.Interacting;
using Data.Engine;
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
  
  public Task<User?> GetByAuthData(
    string name, 
    Password password,
    CancellationToken cancellationToken)
  {
    return context.Users
      .SingleOrDefaultAsync(u => u.Name == name && u.Password.Equals(password), 
        cancellationToken);
  }

  public Task Create(User user, CancellationToken cancellationToken)
  {
    context.Users.AddAsync(user, cancellationToken);

    return context.SaveChangesAsync(cancellationToken);
  }
}