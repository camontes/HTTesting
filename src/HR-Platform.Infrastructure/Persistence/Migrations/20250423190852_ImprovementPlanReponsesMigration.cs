using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ImprovementPlanReponsesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImprovementPlanResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImprovementPlanTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskResponse = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImprovementPlanResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImprovementPlanResponse_ImprovementPlanTasks_ImprovementPla~",
                        column: x => x.ImprovementPlanTaskId,
                        principalTable: "ImprovementPlanTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImprovementPlanResponseFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImprovementPlanResponseId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    UrlFile = table.Column<string>(type: "text", nullable: false),
                    EmailWhoChanged = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameWhoChanged = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UrlPhotoWhoChanged = table.Column<string>(type: "text", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImprovementPlanResponseFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImprovementPlanResponseFile_ImprovementPlanResponse_Improve~",
                        column: x => x.ImprovementPlanResponseId,
                        principalTable: "ImprovementPlanResponse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImprovementPlanResponse_ImprovementPlanTaskId",
                table: "ImprovementPlanResponse",
                column: "ImprovementPlanTaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImprovementPlanResponseFile_ImprovementPlanResponseId",
                table: "ImprovementPlanResponseFile",
                column: "ImprovementPlanResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImprovementPlanResponseFile");

            migrationBuilder.DropTable(
                name: "ImprovementPlanResponse");
        }
    }
}
