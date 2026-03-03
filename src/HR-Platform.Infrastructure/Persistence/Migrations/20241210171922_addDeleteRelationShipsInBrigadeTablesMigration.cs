using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addDeleteRelationShipsInBrigadeTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "BrigadeAdjustmentId",
                table: "CollaboratorBrigades",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorBrigades_BrigadeAdjustmentId",
                table: "CollaboratorBrigades",
                column: "BrigadeAdjustmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorBrigades_BrigadeAdjustments_BrigadeAdjustmentId",
                table: "CollaboratorBrigades",
                column: "BrigadeAdjustmentId",
                principalTable: "BrigadeAdjustments",
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
                name: "FK_CollaboratorBrigades_BrigadeAdjustments_BrigadeAdjustmentId",
                table: "CollaboratorBrigades");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorBrigades_Collaborators_CollaboratorId",
                table: "CollaboratorBrigades");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorBrigades_BrigadeAdjustmentId",
                table: "CollaboratorBrigades");

            migrationBuilder.DropColumn(
                name: "BrigadeAdjustmentId",
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
    }
}
