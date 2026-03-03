using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStudyTypeAndAreaTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultStudyAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultStudyAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultStudyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultStudyTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultStudyAreas",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Ciencias administrativas, económicas y financieras", "Management, economic and financial sciences" },
                    { 3, "Ciencias de la salud", "Health sciences" },
                    { 4, "Ciencias sociales y humanas", "Social and human sciences" },
                    { 5, "Diseño", "Design" },
                    { 6, "Comunicación", "Communication" },
                    { 7, "Ingeniería y tecnología", "Engineering and technology" },
                    { 8, "Educación", "Education" },
                    { 9, "Derecho", "Law" },
                    { 10, "Empresarial", "Business" },
                    { 11, "Otra", "Other" }
                });

            migrationBuilder.InsertData(
                table: "DefaultStudyTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Educación formal", "Formal education" },
                    { 3, "Educación complementaria", "Complementary education" },
                    { 4, "Certificaciones", "Certifications" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultStudyAreas");

            migrationBuilder.DropTable(
                name: "DefaultStudyTypes");
        }
    }
}
