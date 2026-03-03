using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRiskTypeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RiskTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Riesgos naturales", "Natural risks" },
                    { 2, "Riesgos biologicos", "Biological risks" },
                    { 3, "Riesgos fisicos", "Physical risks" },
                    { 4, "Riesgos químico", "Chemical risks" },
                    { 5, "Riesgos psicosocial", "Psychosocial risks" },
                    { 6, "Riesgos biomecánico", "Biomechanical risks" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiskTypes");
        }
    }
}
