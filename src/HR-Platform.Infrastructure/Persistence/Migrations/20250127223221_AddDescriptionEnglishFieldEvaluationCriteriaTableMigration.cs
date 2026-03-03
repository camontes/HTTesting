using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionEnglishFieldEvaluationCriteriaTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OC_AdaptabilityAndFlexibility_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_AttitudeAndProactivity_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_TeamworkAndCollaboration_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_AchievementGoalsObjectives_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_ObjectiveCriteria_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_PunctualityAdherenceSchedule_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_TechnicalCompetence_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OC_AdaptabilityAndFlexibility_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_AttitudeAndProactivity_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_TeamworkAndCollaboration_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AchievementGoalsObjectives_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_ObjectiveCriteria_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_PunctualityAdherenceSchedule_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_TechnicalCompetence_DescriptionEnglish",
                table: "EvaluationCriterias");
        }
    }
}
