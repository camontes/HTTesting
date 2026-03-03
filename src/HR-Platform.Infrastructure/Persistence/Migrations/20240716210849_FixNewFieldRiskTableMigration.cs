using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixNewFieldRiskTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Risks",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "RiskTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Riesgo fisico");

            migrationBuilder.UpdateData(
                table: "RiskTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Riesgo químico");

            migrationBuilder.UpdateData(
                table: "RiskTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Riesgo psicosocial");

            migrationBuilder.UpdateData(
                table: "RiskTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Riesgo biomecánico");

            migrationBuilder.InsertData(
                table: "RiskTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[] { 7, "Ninguno", "None" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RiskTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Risks");

            migrationBuilder.UpdateData(
                table: "RiskTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Riesgos fisicos");

            migrationBuilder.UpdateData(
                table: "RiskTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Riesgos químico");

            migrationBuilder.UpdateData(
                table: "RiskTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Riesgos psicosocial");

            migrationBuilder.UpdateData(
                table: "RiskTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Riesgos biomecánico");
        }
    }
}
