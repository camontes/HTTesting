using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewDefaultProfessionalAdviceTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultProfessionalAdvice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameAcronyms = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameAcronymsEnglish = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultProfessionalAdvice", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultProfessionalAdvice",
                columns: new[] { "Id", "Name", "NameAcronyms", "NameAcronymsEnglish", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "", "", "None" },
                    { 2, "Consejo profesional nacional de ingeniería", "COPNIA", "NEPC", "National Engineering Professional Council" },
                    { 3, "Colegio de psicólogos", "COLPSIC", "COLPSIC", "College of psychologists" },
                    { 4, "Consejo técnico de contaduría publica", "CTCP", "PATA", "Public accounting technical advice" },
                    { 5, "Junta central de contadores", "JCC", "CBA", "Central Board of Accountants" },
                    { 6, "Colegio profesional de administración de empresas", "CEPAE", "PCBA", "Professional College of Business Administration" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultProfessionalAdvice");
        }
    }
}
