using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultQuestionTypeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultQuestionTypeId",
                table: "CollaboratorEducations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DefaultQuestionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultQuestionTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultQuestionTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Opción múltiple", "Multiple choice" },
                    { 3, "Única opción", "Only option" },
                    { 4, "Texto largo", "Long text" },
                    { 5, "Texto corto", "Short text" },
                    { 6, "Calificación", "Qualification" },
                    { 7, "Listado", "Listing" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEducations_DefaultQuestionTypeId",
                table: "CollaboratorEducations",
                column: "DefaultQuestionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultQuestionTypes_DefaultQuestion~",
                table: "CollaboratorEducations",
                column: "DefaultQuestionTypeId",
                principalTable: "DefaultQuestionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultQuestionTypes_DefaultQuestion~",
                table: "CollaboratorEducations");

            migrationBuilder.DropTable(
                name: "DefaultQuestionTypes");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorEducations_DefaultQuestionTypeId",
                table: "CollaboratorEducations");

            migrationBuilder.DropColumn(
                name: "DefaultQuestionTypeId",
                table: "CollaboratorEducations");
        }
    }
}
