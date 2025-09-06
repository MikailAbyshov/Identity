using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Engine;

internal sealed class IdentityContext : DbContext, IIdentityContext
{
  public DbSet<User> Users { get; set; }

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