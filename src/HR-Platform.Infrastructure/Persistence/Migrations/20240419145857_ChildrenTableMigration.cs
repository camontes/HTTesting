using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChildrenTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_FamilyCompositions_FamilyCompositionId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_FamilyCompositionId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "FamilyCompositionId",
                table: "Collaborators");

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Children_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Children_CollaboratorId",
                table: "Children",
                column: "CollaboratorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.AddColumn<Guid>(
                name: "FamilyCompositionId",
                table: "Collaborators",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_FamilyCompositionId",
                table: "Collaborators",
                column: "FamilyCompositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_FamilyCompositions_FamilyCompositionId",
                table: "Collaborators",
                column: "FamilyCompositionId",
                principalTable: "FamilyCompositions",
                principalColumn: "Id");
        }
    }
}
