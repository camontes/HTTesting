using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBrigadeInventoryFileTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDateFile",
                table: "BrigadeInventories");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "BrigadeInventories");

            migrationBuilder.DropColumn(
                name: "FileURL",
                table: "BrigadeInventories");

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "BrigadeInventories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "BrigadeInventoryFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BrigadeInventoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UrlFile = table.Column<string>(type: "text", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrigadeInventoryFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrigadeInventoryFiles_BrigadeInventories_BrigadeInventoryId",
                        column: x => x.BrigadeInventoryId,
                        principalTable: "BrigadeInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrigadeInventoryFiles_BrigadeInventoryId",
                table: "BrigadeInventoryFiles",
                column: "BrigadeInventoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrigadeInventoryFiles");

            migrationBuilder.AlterColumn<string>(
                name: "Observations",
                table: "BrigadeInventories",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateFile",
                table: "BrigadeInventories",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "BrigadeInventories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileURL",
                table: "BrigadeInventories",
                type: "character varying(800)",
                maxLength: 800,
                nullable: false,
                defaultValue: "");
        }
    }
}
