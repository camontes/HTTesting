using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationshipsCollaboratorBrigadeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorBrigades_Collaborators_CollaboratorId",
                table: "CollaboratorBrigades");

            migrationBuilder.RenameColumn(
                name: "CollaboratorId",
                table: "CollaboratorBrigades",
                newName: "BrigadeMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorBrigades_CollaboratorId",
                table: "CollaboratorBrigades",
                newName: "IX_CollaboratorBrigades_BrigadeMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorBrigades_BrigadeMembers_BrigadeMemberId",
                table: "CollaboratorBrigades",
                column: "BrigadeMemberId",
                principalTable: "BrigadeMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorBrigades_BrigadeMembers_BrigadeMemberId",
                table: "CollaboratorBrigades");

            migrationBuilder.RenameColumn(
                name: "BrigadeMemberId",
                table: "CollaboratorBrigades",
                newName: "CollaboratorId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorBrigades_BrigadeMemberId",
                table: "CollaboratorBrigades",
                newName: "IX_CollaboratorBrigades_CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorBrigades_Collaborators_CollaboratorId",
                table: "CollaboratorBrigades",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
