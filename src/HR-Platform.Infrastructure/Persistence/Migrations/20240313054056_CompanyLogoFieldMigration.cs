using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CompanyLogoFieldMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameEnglish",
                value: "HR Superadministrator");

            migrationBuilder.UpdateData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameEnglish",
                value: "HR Administrator");

            migrationBuilder.InsertData(
                table: "DefaultRoles",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[] { 3, "Colaborador", "Employee" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Companies");

            migrationBuilder.UpdateData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameEnglish",
                value: "Superadministrator RH");

            migrationBuilder.UpdateData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameEnglish",
                value: "RH Administrator");
        }
    }
}
