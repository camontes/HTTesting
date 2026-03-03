using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDescriptionFieldsPermissionGroupMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PermissionGroups");

            migrationBuilder.DropColumn(
                name: "DescriptionEnglish",
                table: "PermissionGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PermissionGroups",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEnglish",
                table: "PermissionGroups",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "PermissionGroups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "DescriptionEnglish" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "PermissionGroups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "DescriptionEnglish" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "PermissionGroups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "DescriptionEnglish" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "PermissionGroups",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "DescriptionEnglish" },
                values: new object[] { "", "" });
        }
    }
}
