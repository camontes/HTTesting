using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustInductionTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InductionFiles_Collaborators_CollaboratorId",
                table: "InductionFiles");

            migrationBuilder.DropIndex(
                name: "IX_InductionFiles_CollaboratorId",
                table: "InductionFiles");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "InductionFiles");

            migrationBuilder.CreateTable(
                name: "CollaboratorInductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    InductionId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorInductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorInductions_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorInductions_Induction_InductionId",
                        column: x => x.InductionId,
                        principalTable: "Induction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorInductions_CollaboratorId",
                table: "CollaboratorInductions",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorInductions_InductionId",
                table: "CollaboratorInductions",
                column: "InductionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorInductions");

            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorId",
                table: "InductionFiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InductionFiles_CollaboratorId",
                table: "InductionFiles",
                column: "CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_InductionFiles_Collaborators_CollaboratorId",
                table: "InductionFiles",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
