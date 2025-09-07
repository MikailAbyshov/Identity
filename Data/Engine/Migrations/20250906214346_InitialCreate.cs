using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Data.Engine.Migrations
{
  /// <inheritdoc />
  public partial class InitialCreate : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "users",
          columns: table => new
          {
            id = table.Column<Guid>(type: "uuid", nullable: false),
            password_hash = table.Column<string>(type: "text", nullable: false),
            password_salt = table.Column<string>(type: "text", nullable: true),
            external_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
            name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
            created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            created_by = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("pk_users", x => x.id);
          });

      migrationBuilder.CreateIndex(
          name: "ix_users_name_password_hash_password_salt",
          table: "users",
          columns: new[] { "name", "password_hash", "password_salt" },
          filter: "deleted_at IS NULL");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "users");
    }
  }
}