using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DefaultAreaTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultArea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultArea", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultArea",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Operaciones", "Operations" },
                    { 3, "Talento Humano", "Human Talent" },
                    { 4, "Infraestructura", "Infrastructure" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultArea");

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Operaciones", "Operations" },
                    { 3, "Talento Humano", "Human Talent" },
                    { 4, "Infraestructura", "Infrastructure" }
                });
        }
    }
}
