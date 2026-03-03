using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixAllImprovementPlansMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImprovementPlanFiles");

            migrationBuilder.DropColumn(
                name: "TaskDescription",
                table: "ImprovementPlans");

            migrationBuilder.CreateTable(
                name: "ImprovementPlanTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImprovementPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskDescription = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImprovementPlanTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImprovementPlanTasks_ImprovementPlans_ImprovementPlanId",
                        column: x => x.ImprovementPlanId,
                        principalTable: "ImprovementPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImprovementPlanTaskFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImprovementPlanTaskId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_ImprovementPlanTaskFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImprovementPlanTaskFiles_ImprovementPlanTasks_ImprovementPl~",
                        column: x => x.ImprovementPlanTaskId,
                        principalTable: "ImprovementPlanTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImprovementPlanTaskFiles_ImprovementPlanTaskId",
                table: "ImprovementPlanTaskFiles",
                column: "ImprovementPlanTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ImprovementPlanTasks_ImprovementPlanId",
                table: "ImprovementPlanTasks",
                column: "ImprovementPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImprovementPlanTaskFiles");

            migrationBuilder.DropTable(
                name: "ImprovementPlanTasks");

            migrationBuilder.AddColumn<string>(
                name: "TaskDescription",
                table: "ImprovementPlans",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ImprovementPlanFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImprovementPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EmailWhoChangedByTH = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    NameWhoChangedByTH = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UrlFile = table.Column<string>(type: "text", nullable: false),
                    UrlPhotoWhoChangedByTH = table.Column<string>(type: "text", nullable: false)
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
    }
}
