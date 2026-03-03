using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEducationStageTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultEducationStageId",
                table: "CollaboratorEducations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DefaultEducationStages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultEducationStages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultEducationStages",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Actualmente Estudiando", "Currently Studying" },
                    { 2, "Aplazado", "deferred" },
                    { 3, "Sin Finalizar", "Unfinished" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEducations_DefaultEducationStageId",
                table: "CollaboratorEducations",
                column: "DefaultEducationStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultEducationStages_DefaultEducat~",
                table: "CollaboratorEducations",
                column: "DefaultEducationStageId",
                principalTable: "DefaultEducationStages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultEducationStages_DefaultEducat~",
                table: "CollaboratorEducations");

            migrationBuilder.DropTable(
                name: "DefaultEducationStages");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorEducations_DefaultEducationStageId",
                table: "CollaboratorEducations");

            migrationBuilder.DropColumn(
                name: "DefaultEducationStageId",
                table: "CollaboratorEducations");
        }
    }
}
