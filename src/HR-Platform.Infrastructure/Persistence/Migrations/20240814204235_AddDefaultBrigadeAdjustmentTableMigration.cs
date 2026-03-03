using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultBrigadeAdjustmentTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultBrigadeAdjustments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultBrigadeAdjustments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultBrigadeAdjustments",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Brigada de primeros auxilios", "First aid brigade" },
                    { 3, "Brigada contraincendios", "Fire Brigade" },
                    { 4, "Brigada de evacuación", "Evacuation brigade" }
                });

            migrationBuilder.UpdateData(
                table: "DefaultEmergencyPlanTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "En caso de emergencia", "In case of emergency" });

            migrationBuilder.UpdateData(
                table: "DefaultEmergencyPlanTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "NameEnglish",
                value: "Drills");

            migrationBuilder.InsertData(
                table: "DefaultRiskTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[] { 8, "Riesgo de condiciones de seguridad", "Risk of safety conditions" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultBrigadeAdjustments");

            migrationBuilder.DeleteData(
                table: "DefaultRiskTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "DefaultEmergencyPlanTypes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Caso de emergencia", "Emergency case" });

            migrationBuilder.UpdateData(
                table: "DefaultEmergencyPlanTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "NameEnglish",
                value: "Simulations");
        }
    }
}
