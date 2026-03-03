using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultFileTypeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeFileName",
                table: "OccupationalTests");

            migrationBuilder.AddColumn<int>(
                name: "DefaultFileTypeId",
                table: "OccupationalTests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DefaultFileTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultFileTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultFileTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Certificado ", "Certificate" },
                    { 2, "Informe de resultados", "Report of results" },
                    { 3, "Otro ", "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OccupationalTests_DefaultFileTypeId",
                table: "OccupationalTests",
                column: "DefaultFileTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OccupationalTests_DefaultFileTypes_DefaultFileTypeId",
                table: "OccupationalTests",
                column: "DefaultFileTypeId",
                principalTable: "DefaultFileTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OccupationalTests_DefaultFileTypes_DefaultFileTypeId",
                table: "OccupationalTests");

            migrationBuilder.DropTable(
                name: "DefaultFileTypes");

            migrationBuilder.DropIndex(
                name: "IX_OccupationalTests_DefaultFileTypeId",
                table: "OccupationalTests");

            migrationBuilder.DropColumn(
                name: "DefaultFileTypeId",
                table: "OccupationalTests");

            migrationBuilder.AddColumn<string>(
                name: "TypeFileName",
                table: "OccupationalTests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
