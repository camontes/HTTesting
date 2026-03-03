using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FamilyCompositionTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FamilyCompositionId",
                table: "Collaborators",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FamilyCompositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyCompositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyCompositions_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_FamilyCompositionId",
                table: "Collaborators",
                column: "FamilyCompositionId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyCompositions_CollaboratorId",
                table: "FamilyCompositions",
                column: "CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_FamilyCompositions_FamilyCompositionId",
                table: "Collaborators",
                column: "FamilyCompositionId",
                principalTable: "FamilyCompositions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_FamilyCompositions_FamilyCompositionId",
                table: "Collaborators");

            migrationBuilder.DropTable(
                name: "FamilyCompositions");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_FamilyCompositionId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "FamilyCompositionId",
                table: "Collaborators");
        }
    }
}
