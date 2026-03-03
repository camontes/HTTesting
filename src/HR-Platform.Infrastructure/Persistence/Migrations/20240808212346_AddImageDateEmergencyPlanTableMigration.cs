using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImageDateEmergencyPlanTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DefaultContractTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<DateTime>(
                name: "ImageCreationTime",
                table: "EmergencyPlans",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "DefaultContractTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Aprendizaje", "Apprenticeship " });

            migrationBuilder.UpdateData(
                table: "DefaultRiskTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Riesgos biológicos");

            migrationBuilder.UpdateData(
                table: "DefaultRiskTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Riesgo físico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCreationTime",
                table: "EmergencyPlans");

            migrationBuilder.UpdateData(
                table: "DefaultContractTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Obra Labor", "Labor" });

            migrationBuilder.InsertData(
                table: "DefaultContractTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[] { 4, "Prestaciones", "Service" });

            migrationBuilder.UpdateData(
                table: "DefaultRiskTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Riesgos biologicos");

            migrationBuilder.UpdateData(
                table: "DefaultRiskTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Riesgo fisico");
        }
    }
}
