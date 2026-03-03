using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixDefaultCriteriasTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpperScore",
                value: 25);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 26, 50 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 51, 75 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 76, 100 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpperScore",
                value: 25);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 26, 50 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 51, 75 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 76, 100 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 9,
                column: "UpperScore",
                value: 25);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 26, 50 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 51, 75 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 76, 100 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 13,
                column: "UpperScore",
                value: 25);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 26, 50 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 51, 75 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 76, 100 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 17,
                column: "UpperScore",
                value: 25);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 26, 50 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 51, 75 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 76, 100 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 21,
                column: "UpperScore",
                value: 25);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 26, 50 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 51, 75 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 76, 100 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 25,
                column: "UpperScore",
                value: 25);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 26, 50 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 51, 75 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 76, 100 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpperScore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpperScore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 9,
                column: "UpperScore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 13,
                column: "UpperScore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 17,
                column: "UpperScore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 21,
                column: "UpperScore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 25,
                column: "UpperScore",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScore",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "LowerScore", "UpperScore" },
                values: new object[] { 0, 0 });
        }
    }
}
