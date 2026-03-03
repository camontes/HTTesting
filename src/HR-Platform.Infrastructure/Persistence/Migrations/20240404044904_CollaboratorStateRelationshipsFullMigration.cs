using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CollaboratorStateRelationshipsFullMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_CollaboratorStates_CollaboratorStateId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<int>(
                name: "CollaboratorStateId",
                table: "Collaborators",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_CollaboratorStates_CollaboratorStateId",
                table: "Collaborators",
                column: "CollaboratorStateId",
                principalTable: "CollaboratorStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_CollaboratorStates_CollaboratorStateId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<int>(
                name: "CollaboratorStateId",
                table: "Collaborators",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_CollaboratorStates_CollaboratorStateId",
                table: "Collaborators",
                column: "CollaboratorStateId",
                principalTable: "CollaboratorStates",
                principalColumn: "Id");
        }
    }
}
