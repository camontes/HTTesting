using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FormAnswerGroupStatesRelationshipsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FormAnswerGroups_FormAnswerGroupStateId",
                table: "FormAnswerGroups",
                column: "FormAnswerGroupStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswerGroups_FormAnswerGroupStates_FormAnswerGroupState~",
                table: "FormAnswerGroups",
                column: "FormAnswerGroupStateId",
                principalTable: "FormAnswerGroupStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswerGroups_FormAnswerGroupStates_FormAnswerGroupState~",
                table: "FormAnswerGroups");

            migrationBuilder.DropIndex(
                name: "IX_FormAnswerGroups_FormAnswerGroupStateId",
                table: "FormAnswerGroups");
        }
    }
}
