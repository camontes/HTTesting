using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class IsVisibleSurveyFieldMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestion_SurveyQuestionMandatoryTypes_SurveyQuestionM~",
                table: "SurveyQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestion_SurveyQuestionTypes_SurveyQuestionTypeId",
                table: "SurveyQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestion_Surveys_SurveyId",
                table: "SurveyQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestion",
                table: "SurveyQuestion");

            migrationBuilder.RenameTable(
                name: "SurveyQuestion",
                newName: "SurveyQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestion_SurveyQuestionTypeId",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_SurveyQuestionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestion_SurveyQuestionMandatoryTypeId",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_SurveyQuestionMandatoryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestion_SurveyId",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_SurveyId");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Surveys",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_SurveyQuestionMandatoryTypes_SurveyQuestion~",
                table: "SurveyQuestions",
                column: "SurveyQuestionMandatoryTypeId",
                principalTable: "SurveyQuestionMandatoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_SurveyQuestionTypes_SurveyQuestionTypeId",
                table: "SurveyQuestions",
                column: "SurveyQuestionTypeId",
                principalTable: "SurveyQuestionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveyId",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_SurveyQuestionMandatoryTypes_SurveyQuestion~",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_SurveyQuestionTypes_SurveyQuestionTypeId",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveyId",
                table: "SurveyQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestions",
                table: "SurveyQuestions");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Surveys");

            migrationBuilder.RenameTable(
                name: "SurveyQuestions",
                newName: "SurveyQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_SurveyQuestionTypeId",
                table: "SurveyQuestion",
                newName: "IX_SurveyQuestion_SurveyQuestionTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_SurveyQuestionMandatoryTypeId",
                table: "SurveyQuestion",
                newName: "IX_SurveyQuestion_SurveyQuestionMandatoryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_SurveyId",
                table: "SurveyQuestion",
                newName: "IX_SurveyQuestion_SurveyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestion",
                table: "SurveyQuestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestion_SurveyQuestionMandatoryTypes_SurveyQuestionM~",
                table: "SurveyQuestion",
                column: "SurveyQuestionMandatoryTypeId",
                principalTable: "SurveyQuestionMandatoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestion_SurveyQuestionTypes_SurveyQuestionTypeId",
                table: "SurveyQuestion",
                column: "SurveyQuestionTypeId",
                principalTable: "SurveyQuestionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestion_Surveys_SurveyId",
                table: "SurveyQuestion",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
