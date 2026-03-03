using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixDefaultEvaluationCriteriasValues2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 17,
                column: "DescriptionEnglish",
                value: "Frequent difficulties working as part of a team.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 17,
                column: "DescriptionEnglish",
                value: "Difficulties adapting to changes");
        }
    }
}
