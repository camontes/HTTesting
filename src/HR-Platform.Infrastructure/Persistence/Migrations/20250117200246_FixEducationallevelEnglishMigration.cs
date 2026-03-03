using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixEducationallevelEnglishMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultEducationalLevels",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameEnglish",
                value: "Technical");

            migrationBuilder.UpdateData(
                table: "DefaultEducationalLevels",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameEnglish",
                value: "Technologist");

            migrationBuilder.UpdateData(
                table: "DefaultEducationalLevels",
                keyColumn: "Id",
                keyValue: 4,
                column: "NameEnglish",
                value: "College degree");

            migrationBuilder.UpdateData(
                table: "DefaultEducationalLevels",
                keyColumn: "Id",
                keyValue: 5,
                column: "NameEnglish",
                value: "Postgraduate degree");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultEducationalLevels",
                keyColumn: "Id",
                keyValue: 2,
                column: "NameEnglish",
                value: "Técnico");

            migrationBuilder.UpdateData(
                table: "DefaultEducationalLevels",
                keyColumn: "Id",
                keyValue: 3,
                column: "NameEnglish",
                value: "Tecnólogo");

            migrationBuilder.UpdateData(
                table: "DefaultEducationalLevels",
                keyColumn: "Id",
                keyValue: 4,
                column: "NameEnglish",
                value: "Pregrado");

            migrationBuilder.UpdateData(
                table: "DefaultEducationalLevels",
                keyColumn: "Id",
                keyValue: 5,
                column: "NameEnglish",
                value: "Posgrado");
        }
    }
}
