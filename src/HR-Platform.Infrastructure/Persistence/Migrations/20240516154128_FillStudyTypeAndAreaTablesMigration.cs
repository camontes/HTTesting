using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FillStudyTypeAndAreaTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DefaultStudyTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DefaultStudyTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DefaultStudyTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DefaultStudyTypes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
