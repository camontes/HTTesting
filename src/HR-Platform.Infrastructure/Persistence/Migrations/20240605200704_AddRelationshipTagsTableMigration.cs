using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipTagsTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Tag_TagId1",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_TagId1",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "TagId1",
                table: "Collaborators");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_TagId",
                table: "Collaborators",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Tag_TagId",
                table: "Collaborators",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Tag_TagId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_TagId",
                table: "Collaborators");

            migrationBuilder.AddColumn<Guid>(
                name: "TagId1",
                table: "Collaborators",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_TagId1",
                table: "Collaborators",
                column: "TagId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Tag_TagId1",
                table: "Collaborators",
                column: "TagId1",
                principalTable: "Tag",
                principalColumn: "Id");
        }
    }
}
