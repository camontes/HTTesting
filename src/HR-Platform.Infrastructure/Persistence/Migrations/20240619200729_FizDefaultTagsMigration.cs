using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FizDefaultTagsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DefaultTags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "DefaultTags",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Colaborador Interno", "Internal Collaborator" });

            migrationBuilder.UpdateData(
                table: "DefaultTags",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Colaborador Externo", "External Collaborator" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultTags",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Ninguno", "None" });

            migrationBuilder.UpdateData(
                table: "DefaultTags",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Colaborador Interno", "Internal Collaborator" });

            migrationBuilder.InsertData(
                table: "DefaultTags",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[] { 3, "Colaborador Externo", "External Collaborator" });
        }
    }
}
