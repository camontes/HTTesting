using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImprovementPlanFilesTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailWhoChangedByTH",
                table: "ImprovementPlans");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ImprovementPlans");

            migrationBuilder.DropColumn(
                name: "NameWhoChangedByTH",
                table: "ImprovementPlans");

            migrationBuilder.DropColumn(
                name: "UrlFile",
                table: "ImprovementPlans");

            migrationBuilder.DropColumn(
                name: "UrlPhotoWhoChangedByTH",
                table: "ImprovementPlans");

            migrationBuilder.CreateTable(
                name: "ImprovementPlanFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImprovementPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    UrlFile = table.Column<string>(type: "text", nullable: false),
                    EmailWhoChangedByTH = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameWhoChangedByTH = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UrlPhotoWhoChangedByTH = table.Column<string>(type: "text", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImprovementPlanFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImprovementPlanFiles_ImprovementPlans_ImprovementPlanId",
                        column: x => x.ImprovementPlanId,
                        principalTable: "ImprovementPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImprovementPlanFiles_ImprovementPlanId",
                table: "ImprovementPlanFiles",
                column: "ImprovementPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImprovementPlanFiles");

            migrationBuilder.AddColumn<string>(
                name: "EmailWhoChangedByTH",
                table: "ImprovementPlans",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ImprovementPlans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameWhoChangedByTH",
                table: "ImprovementPlans",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlFile",
                table: "ImprovementPlans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlPhotoWhoChangedByTH",
                table: "ImprovementPlans",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
