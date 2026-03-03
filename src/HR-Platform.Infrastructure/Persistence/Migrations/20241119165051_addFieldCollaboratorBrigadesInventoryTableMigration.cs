using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addFieldCollaboratorBrigadesInventoryTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorBrigade_CollaboratorBrigadeInventory_Collaborat~",
                table: "CollaboratorBrigade");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorBrigade_Collaborators_CollaboratorId",
                table: "CollaboratorBrigade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollaboratorBrigade",
                table: "CollaboratorBrigade");

            migrationBuilder.RenameTable(
                name: "CollaboratorBrigade",
                newName: "CollaboratorBrigades");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorBrigade_CollaboratorId",
                table: "CollaboratorBrigades",
                newName: "IX_CollaboratorBrigades_CollaboratorId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorBrigade_CollaboratorBrigadeInventoryId",
                table: "CollaboratorBrigades",
                newName: "IX_CollaboratorBrigades_CollaboratorBrigadeInventoryId");

            migrationBuilder.AddColumn<bool>(
                name: "SendForAll",
                table: "CollaboratorBrigadeInventory",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AvailableAmount",
                table: "BrigadeInventories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollaboratorBrigades",
                table: "CollaboratorBrigades",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorBrigades_CollaboratorBrigadeInventory_Collabora~",
                table: "CollaboratorBrigades",
                column: "CollaboratorBrigadeInventoryId",
                principalTable: "CollaboratorBrigadeInventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorBrigades_Collaborators_CollaboratorId",
                table: "CollaboratorBrigades",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorBrigades_CollaboratorBrigadeInventory_Collabora~",
                table: "CollaboratorBrigades");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorBrigades_Collaborators_CollaboratorId",
                table: "CollaboratorBrigades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollaboratorBrigades",
                table: "CollaboratorBrigades");

            migrationBuilder.DropColumn(
                name: "SendForAll",
                table: "CollaboratorBrigadeInventory");

            migrationBuilder.DropColumn(
                name: "AvailableAmount",
                table: "BrigadeInventories");

            migrationBuilder.RenameTable(
                name: "CollaboratorBrigades",
                newName: "CollaboratorBrigade");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorBrigades_CollaboratorId",
                table: "CollaboratorBrigade",
                newName: "IX_CollaboratorBrigade_CollaboratorId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorBrigades_CollaboratorBrigadeInventoryId",
                table: "CollaboratorBrigade",
                newName: "IX_CollaboratorBrigade_CollaboratorBrigadeInventoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollaboratorBrigade",
                table: "CollaboratorBrigade",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorBrigade_CollaboratorBrigadeInventory_Collaborat~",
                table: "CollaboratorBrigade",
                column: "CollaboratorBrigadeInventoryId",
                principalTable: "CollaboratorBrigadeInventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorBrigade_Collaborators_CollaboratorId",
                table: "CollaboratorBrigade",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
