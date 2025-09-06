using Data.Records;
using Microsoft.EntityFrameworkCore;

namespace Data.Engine;

internal sealed class IdentityContext(DbContextOptions options) : DbContext(options), IIdentityContext
{
  public DbSet<UserRecord> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
    optionsBuilder.UseSnakeCaseNamingConvention();
  }
}