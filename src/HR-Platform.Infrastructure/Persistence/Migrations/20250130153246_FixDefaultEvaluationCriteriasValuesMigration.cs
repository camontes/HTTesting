using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixDefaultEvaluationCriteriasValuesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 3,
                column: "DescriptionEnglish",
                value: "Meets most goals.");

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriterias",
                keyColumn: "Id",
                keyValue: 4,
                column: "NameEnglish",
                value: "Delivery of projects and tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 3,
                column: "DescriptionEnglish",
                value: "Meets some goals, but needs improvement.");

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriterias",
                keyColumn: "Id",
                keyValue: 4,
                column: "NameEnglish",
                value: "Punctuality and compliance with schedule");
        }
    }
}
