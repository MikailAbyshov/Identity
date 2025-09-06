using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Data.Engine;

/// <summary>
/// Контекст Identity
/// </summary>
public interface IIdentityContext
{
  /// <summary>
  /// Пользователи сервиса
  /// </summary>
  DbSet<User> Users { get; set; }
  
  DbSet<TRecord> Set<TRecord>() where TRecord : class;

  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
  
  DatabaseFacade Database { get; }
}