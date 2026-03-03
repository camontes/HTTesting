using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FormAnswerGroupsRelationshipsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_FormAnswerGroup_FormAnswerGroupId1",
                table: "FormAnswers");

            migrationBuilder.DropIndex(
                name: "IX_FormAnswers_FormAnswerGroupId1",
                table: "FormAnswers");

            migrationBuilder.DropColumn(
                name: "FormAnswerGroupId1",
                table: "FormAnswers");

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswers_FormAnswerGroupId",
                table: "FormAnswers",
                column: "FormAnswerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswerGroup_FormId",
                table: "FormAnswerGroup",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswerGroup_Forms_FormId",
                table: "FormAnswerGroup",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_FormAnswerGroup_FormAnswerGroupId",
                table: "FormAnswers",
                column: "FormAnswerGroupId",
                principalTable: "FormAnswerGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswerGroup_Forms_FormId",
                table: "FormAnswerGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_FormAnswerGroup_FormAnswerGroupId",
                table: "FormAnswers");

            migrationBuilder.DropIndex(
                name: "IX_FormAnswers_FormAnswerGroupId",
                table: "FormAnswers");

            migrationBuilder.DropIndex(
                name: "IX_FormAnswerGroup_FormId",
                table: "FormAnswerGroup");

            migrationBuilder.AddColumn<Guid>(
                name: "FormAnswerGroupId1",
                table: "FormAnswers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswers_FormAnswerGroupId1",
                table: "FormAnswers",
                column: "FormAnswerGroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_FormAnswerGroup_FormAnswerGroupId1",
                table: "FormAnswers",
                column: "FormAnswerGroupId1",
                principalTable: "FormAnswerGroup",
                principalColumn: "Id");
        }
    }
}
