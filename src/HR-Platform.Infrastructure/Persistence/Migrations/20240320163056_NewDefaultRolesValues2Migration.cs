using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewDefaultRolesValues2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameEnglish",
                value: "Superadministrator HR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "NameEnglish",
                value: "HR Superadministrator");
        }
    }
}
