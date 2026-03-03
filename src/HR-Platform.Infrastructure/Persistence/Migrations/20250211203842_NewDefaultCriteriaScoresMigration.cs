using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewDefaultCriteriaScoresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DefaultEvaluationCriteriaScores",
                columns: new[] { "Id", "DefaultEvaluationCriteriaId", "Description", "DescriptionEnglish", "IsDeleteable", "IsEditable", "IsForDefaultCriterias", "LowerScore", "UpperScore" },
                values: new object[,]
                {
                    { 29, 8, "No cumple ", "Does not meet", false, false, false, 0, 25 },
                    { 30, 8, "Cumple algunas veces", "Meets sometimes", false, false, false, 26, 50 },
                    { 31, 8, "Cumple la mayoría de las veces", "Meets most of the time", false, false, false, 51, 75 },
                    { 32, 8, "Cumple todas las veces", "Meets all the time", false, false, false, 76, 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 32);
        }
    }
}
