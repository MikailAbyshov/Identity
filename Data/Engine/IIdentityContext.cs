using Data.Records;
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
  DbSet<UserRecord> Users { get; set; }

  DbSet<TRecord> Set<TRecord>() where TRecord : class;

  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

  DatabaseFacade Database { get; }
}