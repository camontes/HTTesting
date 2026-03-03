using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixDefaultEvaluationCriteriasMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationCriteriaScore_EvaluationCriterias_EvaluationCrite~",
                table: "EvaluationCriteriaScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluationCriteriaScore",
                table: "EvaluationCriteriaScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultEvaluationCriteriaScore",
                table: "DefaultEvaluationCriteriaScore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultEvaluationCriteria",
                table: "DefaultEvaluationCriteria");

            migrationBuilder.RenameTable(
                name: "EvaluationCriteriaScore",
                newName: "EvaluationCriteriaScores");

            migrationBuilder.RenameTable(
                name: "DefaultEvaluationCriteriaScore",
                newName: "DefaultEvaluationCriteriaScores");

            migrationBuilder.RenameTable(
                name: "DefaultEvaluationCriteria",
                newName: "DefaultEvaluationCriterias");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluationCriteriaScore_EvaluationCriteriaId",
                table: "EvaluationCriteriaScores",
                newName: "IX_EvaluationCriteriaScores_EvaluationCriteriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluationCriteriaScores",
                table: "EvaluationCriteriaScores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultEvaluationCriteriaScores",
                table: "DefaultEvaluationCriteriaScores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultEvaluationCriterias",
                table: "DefaultEvaluationCriterias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationCriteriaScores_EvaluationCriterias_EvaluationCrit~",
                table: "EvaluationCriteriaScores",
                column: "EvaluationCriteriaId",
                principalTable: "EvaluationCriterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationCriteriaScores_EvaluationCriterias_EvaluationCrit~",
                table: "EvaluationCriteriaScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluationCriteriaScores",
                table: "EvaluationCriteriaScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultEvaluationCriteriaScores",
                table: "DefaultEvaluationCriteriaScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DefaultEvaluationCriterias",
                table: "DefaultEvaluationCriterias");

            migrationBuilder.RenameTable(
                name: "EvaluationCriteriaScores",
                newName: "EvaluationCriteriaScore");

            migrationBuilder.RenameTable(
                name: "DefaultEvaluationCriteriaScores",
                newName: "DefaultEvaluationCriteriaScore");

            migrationBuilder.RenameTable(
                name: "DefaultEvaluationCriterias",
                newName: "DefaultEvaluationCriteria");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluationCriteriaScores_EvaluationCriteriaId",
                table: "EvaluationCriteriaScore",
                newName: "IX_EvaluationCriteriaScore_EvaluationCriteriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluationCriteriaScore",
                table: "EvaluationCriteriaScore",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultEvaluationCriteriaScore",
                table: "DefaultEvaluationCriteriaScore",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DefaultEvaluationCriteria",
                table: "DefaultEvaluationCriteria",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationCriteriaScore_EvaluationCriterias_EvaluationCrite~",
                table: "EvaluationCriteriaScore",
                column: "EvaluationCriteriaId",
                principalTable: "EvaluationCriterias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
