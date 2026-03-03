using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustEvaluationCriteriaTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SC_ObjectiveCriteria_Value",
                table: "EvaluationCriterias",
                newName: "SC_DeliveryProjectsAndTasks_Value");

            migrationBuilder.RenameColumn(
                name: "SC_ObjectiveCriteria_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "SC_DeliveryProjectsAndTasks_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_ObjectiveCriteria_Description",
                table: "EvaluationCriterias",
                newName: "SC_DeliveryProjectsAndTasks_Description");

            migrationBuilder.RenameColumn(
                name: "SC_ObjectiveCriteriaEnglish",
                table: "EvaluationCriterias",
                newName: "SC_DeliveryProjectsAndTasksEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_ObjectiveCriteria",
                table: "EvaluationCriterias",
                newName: "SC_DeliveryProjectsAndTasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SC_DeliveryProjectsAndTasks_Value",
                table: "EvaluationCriterias",
                newName: "SC_ObjectiveCriteria_Value");

            migrationBuilder.RenameColumn(
                name: "SC_DeliveryProjectsAndTasks_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "SC_ObjectiveCriteria_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_DeliveryProjectsAndTasks_Description",
                table: "EvaluationCriterias",
                newName: "SC_ObjectiveCriteria_Description");

            migrationBuilder.RenameColumn(
                name: "SC_DeliveryProjectsAndTasksEnglish",
                table: "EvaluationCriterias",
                newName: "SC_ObjectiveCriteriaEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_DeliveryProjectsAndTasks",
                table: "EvaluationCriterias",
                newName: "SC_ObjectiveCriteria");
        }
    }
}
