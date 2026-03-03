using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustInductionFileAndDeleteCollaboratorInductionTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_HealthEntity_HealthEntityId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthEntity_Companies_CompanyId",
                table: "HealthEntity");

            migrationBuilder.DropTable(
                name: "CollaboratorInductions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthEntity",
                table: "HealthEntity");

            migrationBuilder.RenameTable(
                name: "HealthEntity",
                newName: "HealthEntities");

            migrationBuilder.RenameIndex(
                name: "IX_HealthEntity_CompanyId",
                table: "HealthEntities",
                newName: "IX_HealthEntities_CompanyId");

            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorId",
                table: "InductionFiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthEntities",
                table: "HealthEntities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InductionFiles_CollaboratorId",
                table: "InductionFiles",
                column: "CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_HealthEntities_HealthEntityId",
                table: "Collaborators",
                column: "HealthEntityId",
                principalTable: "HealthEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthEntities_Companies_CompanyId",
                table: "HealthEntities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InductionFiles_Collaborators_CollaboratorId",
                table: "InductionFiles",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_HealthEntities_HealthEntityId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthEntities_Companies_CompanyId",
                table: "HealthEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_InductionFiles_Collaborators_CollaboratorId",
                table: "InductionFiles");

            migrationBuilder.DropIndex(
                name: "IX_InductionFiles_CollaboratorId",
                table: "InductionFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthEntities",
                table: "HealthEntities");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "InductionFiles");

            migrationBuilder.RenameTable(
                name: "HealthEntities",
                newName: "HealthEntity");

            migrationBuilder.RenameIndex(
                name: "IX_HealthEntities_CompanyId",
                table: "HealthEntity",
                newName: "IX_HealthEntity_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthEntity",
                table: "HealthEntity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CollaboratorInductions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    InductionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_HealthEntity_HealthEntityId",
                table: "Collaborators",
                column: "HealthEntityId",
                principalTable: "HealthEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthEntity_Companies_CompanyId",
                table: "HealthEntity",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
