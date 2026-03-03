using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReseedDefaultPensionTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Ninguno", "None" });

            migrationBuilder.UpdateData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Colfondos", "Colfondos" });

            migrationBuilder.UpdateData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Colpensiones", "Colpensiones" });

            migrationBuilder.UpdateData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Old Mutual", "Old Mutual" });

            migrationBuilder.UpdateData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Porvenir", "Porvenir" });

            migrationBuilder.InsertData(
                table: "DefaultPensions",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[] { 6, "Protección", "Protección" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Colfondos", "Colfondos" });

            migrationBuilder.UpdateData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Colpensiones", "Colpensiones" });

            migrationBuilder.UpdateData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Old Mutual", "Old Mutual" });

            migrationBuilder.UpdateData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Porvenir", "Porvenir" });

            migrationBuilder.UpdateData(
                table: "DefaultPensions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Name", "NameEnglish" },
                values: new object[] { "Protección", "Protección" });
        }
    }
}
