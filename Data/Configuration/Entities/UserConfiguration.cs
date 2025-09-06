using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration.Entities;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.Id);
      
    builder
      .Property(u => u.ExternalId)
      .HasMaxLength(byte.MaxValue)
      .IsRequired();
      
    builder
      .Property(u => u.Id)
      .IsRequired();
      
    builder
      .Property(u => u.Name)
      .HasMaxLength(byte.MaxValue)
      .IsRequired();
      
    builder
      .Property(u => u.Password)
      .IsRequired();
      
    builder
      .Property(u => u.CreatedAt)
      .IsRequired();
      
    builder
      .Property(u => u.UpdatedAt)
      .IsRequired();

    builder
      .Property(u => u.CreatedBy)
      .HasMaxLength(byte.MaxValue)
      .IsRequired();

    builder.HasIndex(u => new { u.Name, u.Password })
      .HasFilter("[DeletedAt] IS NULL");
  }
}