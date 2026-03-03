using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SwapDefaultCriteriaScoresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 9,
                column: "DefaultEvaluationCriteriaId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 10,
                column: "DefaultEvaluationCriteriaId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 11,
                column: "DefaultEvaluationCriteriaId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 12,
                column: "DefaultEvaluationCriteriaId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 13,
                column: "DefaultEvaluationCriteriaId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 14,
                column: "DefaultEvaluationCriteriaId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 15,
                column: "DefaultEvaluationCriteriaId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 16,
                column: "DefaultEvaluationCriteriaId",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 9,
                column: "DefaultEvaluationCriteriaId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 10,
                column: "DefaultEvaluationCriteriaId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 11,
                column: "DefaultEvaluationCriteriaId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 12,
                column: "DefaultEvaluationCriteriaId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 13,
                column: "DefaultEvaluationCriteriaId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 14,
                column: "DefaultEvaluationCriteriaId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 15,
                column: "DefaultEvaluationCriteriaId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 16,
                column: "DefaultEvaluationCriteriaId",
                value: 4);
        }
    }
}
