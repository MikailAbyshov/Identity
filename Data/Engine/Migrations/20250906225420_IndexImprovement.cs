using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Engine.Migrations
{
  /// <inheritdoc />
  public partial class IndexImprovement : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(
          name: "ix_users_name_password_hash_password_salt",
          table: "users");

      migrationBuilder.CreateIndex(
          name: "ix_users_name",
          table: "users",
          column: "name",
          filter: "deleted_at IS NULL");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(
          name: "ix_users_name",
          table: "users");

      migrationBuilder.CreateIndex(
          name: "ix_users_name_password_hash_password_salt",
          table: "users",
          columns: new[] { "name", "password_hash", "password_salt" },
          filter: "deleted_at IS NULL");
    }
  }
}