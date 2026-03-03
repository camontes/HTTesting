using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addDefaultEventReplayTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultEventReplayId",
                table: "CollaboratorContracts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DefaultEventReplays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultEventReplays", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultEventReplays",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "No se repite", "Not repeated" },
                    { 3, "Cada día laborable (lunes - viernes)", "Every working day (Monday - Friday)" },
                    { 4, "Diariamente", "Daily" },
                    { 5, "Semanalmente", "Weekly" },
                    { 6, "Mensualmente", "Monthly" },
                    { 7, "Anualmente", "Annually" },
                    { 8, "Personalizado", "Customized" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorContracts_DefaultEventReplayId",
                table: "CollaboratorContracts",
                column: "DefaultEventReplayId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_DefaultEventReplays_DefaultEventRepla~",
                table: "CollaboratorContracts",
                column: "DefaultEventReplayId",
                principalTable: "DefaultEventReplays",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_DefaultEventReplays_DefaultEventRepla~",
                table: "CollaboratorContracts");

            migrationBuilder.DropTable(
                name: "DefaultEventReplays");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorContracts_DefaultEventReplayId",
                table: "CollaboratorContracts");

            migrationBuilder.DropColumn(
                name: "DefaultEventReplayId",
                table: "CollaboratorContracts");
        }
    }
}
