using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultLanguagesTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultLanguageLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultLanguageLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultLanguageType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultLanguageType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultLanguageLevel",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "A1", "A1" },
                    { 3, "A2", "A2" },
                    { 4, "B1", "B1" },
                    { 5, "B2", "B2" },
                    { 6, "C1", "C1" },
                    { 7, "C2", "C2" },
                    { 8, "Nativo", "Native" }
                });

            migrationBuilder.InsertData(
                table: "DefaultLanguageType",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Español", "Spanish" },
                    { 3, "Inglés", "English" },
                    { 4, "Francés", "French" },
                    { 5, "Alemán", "German" },
                    { 6, "Mandarín", "Chinese" },
                    { 7, "Portugués", "Portuguese" },
                    { 8, "Otro", "Other" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultLanguageLevel");

            migrationBuilder.DropTable(
                name: "DefaultLanguageType");
        }
    }
}
