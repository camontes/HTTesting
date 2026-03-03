using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCollaboratorFieldToFormAnswerTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CollaboratorId",
                table: "FormAnswers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswers_CollaboratorId",
                table: "FormAnswers",
                column: "CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_Collaborators_CollaboratorId",
                table: "FormAnswers",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_Collaborators_CollaboratorId",
                table: "FormAnswers");

            migrationBuilder.DropIndex(
                name: "IX_FormAnswers_CollaboratorId",
                table: "FormAnswers");

            migrationBuilder.DropColumn(
                name: "CollaboratorId",
                table: "FormAnswers");
        }
    }
}
