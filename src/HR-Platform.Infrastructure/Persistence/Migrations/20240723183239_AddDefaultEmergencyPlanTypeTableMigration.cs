using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultEmergencyPlanTypeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultEmergencyPlanTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultEmergencyPlanTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultEmergencyPlanTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Niveles de riesgo", "Risk levels" },
                    { 3, "Punto de encuentro ", "Meeting point" },
                    { 4, "Rutas de evacuación", "Evacuation routes" },
                    { 5, "Caso de emergencia", "Emergency case" },
                    { 6, "Kit de emergencia", "Emergency kit" },
                    { 7, "Simulacros", "Simulations" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultEmergencyPlanTypes");
        }
    }
}
