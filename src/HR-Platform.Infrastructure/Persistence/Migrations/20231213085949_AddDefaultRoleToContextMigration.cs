using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRoleToContextMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultJobArea",
                table: "DefaultJobArea");

            migrationBuilder.RenameTable(
                name: "DefaultJobArea",
                newName: "DefaultJobAreas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultJobAreas",
                table: "DefaultJobAreas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultJobAreas",
                table: "DefaultJobAreas");

            migrationBuilder.RenameTable(
                name: "DefaultJobAreas",
                newName: "DefaultJobArea");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultJobArea",
                table: "DefaultJobArea",
                column: "Id");
        }
    }
}
