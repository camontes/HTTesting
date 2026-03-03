using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewDefaultCriteriasMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsForDefaultCriterias",
                table: "DefaultEvaluationCriteriaScores",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 11,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 12,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 13,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 14,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 15,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 16,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 17,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 18,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 19,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 20,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 21,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 22,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 23,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 24,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 25,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 26,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 27,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.UpdateData(
                table: "DefaultEvaluationCriteriaScores",
                keyColumn: "Id",
                keyValue: 28,
                column: "IsForDefaultCriterias",
                value: true);

            migrationBuilder.InsertData(
                table: "DefaultEvaluationCriterias",
                columns: new[] { "Id", "Description", "DescriptionEnglish", "EvaluationCriteriaTypeId", "IsDeleteable", "IsEditable", "Name", "NameEnglish", "Percentage" },
                values: new object[] { 8, "Criterio por defecto.", "Defualt criteria.", 2, false, false, "Criterio por defecto", "Defualt criteria", 30 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DefaultEvaluationCriterias",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "IsForDefaultCriterias",
                table: "DefaultEvaluationCriteriaScores");
        }
    }
}
