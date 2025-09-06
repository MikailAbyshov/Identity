using Data.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration.Entities;

internal sealed class UserRecordConfiguration : IEntityTypeConfiguration<UserRecord>
{
  public void Configure(EntityTypeBuilder<UserRecord> builder)
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
      .Property(u => u.PasswordHash)
      .IsRequired();
      
    builder
      .Property(u => u.CreatedAt)
      .IsRequired()
      .HasConversion(
        v => v.GetValueOrDefault().UtcDateTime,
        v => new DateTimeOffset(v, TimeSpan.Zero)
      );
      
    builder
      .Property(u => u.UpdatedAt)
      .IsRequired()
      .HasConversion(
        v => v.GetValueOrDefault().UtcDateTime,
        v => new DateTimeOffset(v, TimeSpan.Zero)
      );
    
    builder
      .Property(u => u.DeletedAt)
      .HasConversion(
        v => v.GetValueOrDefault().UtcDateTime,
        v => new DateTimeOffset(v, TimeSpan.Zero)
      );

    builder
      .Property(u => u.CreatedBy)
      .HasMaxLength(byte.MaxValue)
      .IsRequired();

    builder.HasIndex(u => new { u.Name, u.PasswordHash, u.PasswordSalt })
      .HasFilter("deleted_at IS NULL");
  }
}