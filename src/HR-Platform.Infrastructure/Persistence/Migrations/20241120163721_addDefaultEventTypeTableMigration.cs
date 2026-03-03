using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addDefaultEventTypeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultEventTypeId",
                table: "CollaboratorContracts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DefaultEventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultEventTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultEventTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Cumpleaños", "Birthday" },
                    { 3, "Feedback", "Feedback" },
                    { 4, "Pausas activas", "Active breaks" },
                    { 5, "Reunión", "Meeting" },
                    { 6, "Otro", "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorContracts_DefaultEventTypeId",
                table: "CollaboratorContracts",
                column: "DefaultEventTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorContracts_DefaultEventTypes_DefaultEventTypeId",
                table: "CollaboratorContracts",
                column: "DefaultEventTypeId",
                principalTable: "DefaultEventTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorContracts_DefaultEventTypes_DefaultEventTypeId",
                table: "CollaboratorContracts");

            migrationBuilder.DropTable(
                name: "DefaultEventTypes");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorContracts_DefaultEventTypeId",
                table: "CollaboratorContracts");

            migrationBuilder.DropColumn(
                name: "DefaultEventTypeId",
                table: "CollaboratorContracts");
        }
    }
}
