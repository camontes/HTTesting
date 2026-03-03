using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldEvaluationCriteriaTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OC_AdaptabilityAndFlexibilityEnglish",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_AttitudeAndProactivityEnglish",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_TeamworkAndCollaborationEnglish",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_AchievementGoalsObjectivesEnglish",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_ObjectiveCriteriaEnglish",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_PunctualityAdherenceScheduleEnglish",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_TechnicalCompetenceEnglish",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OC_AdaptabilityAndFlexibilityEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_AttitudeAndProactivityEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_TeamworkAndCollaborationEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AchievementGoalsObjectivesEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_ObjectiveCriteriaEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_PunctualityAdherenceScheduleEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_TechnicalCompetenceEnglish",
                table: "EvaluationCriterias");
        }
    }
}
