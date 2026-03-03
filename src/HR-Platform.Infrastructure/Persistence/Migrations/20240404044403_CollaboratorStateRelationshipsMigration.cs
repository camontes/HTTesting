using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CollaboratorStateRelationshipsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollaboratorStateId",
                table: "Collaborators",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_CollaboratorStateId",
                table: "Collaborators",
                column: "CollaboratorStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_CollaboratorStates_CollaboratorStateId",
                table: "Collaborators",
                column: "CollaboratorStateId",
                principalTable: "CollaboratorStates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_CollaboratorStates_CollaboratorStateId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_CollaboratorStateId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "CollaboratorStateId",
                table: "Collaborators");
        }
    }
}
