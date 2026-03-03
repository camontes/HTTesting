using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustNamesEvaluationCriteriaTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SC_TechnicalCompetence_Value",
                table: "EvaluationCriterias",
                newName: "SC_TeamworkAndCollaboration_Value");

            migrationBuilder.RenameColumn(
                name: "SC_TechnicalCompetence_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "SC_TeamworkAndCollaboration_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_TechnicalCompetence_Description",
                table: "EvaluationCriterias",
                newName: "SC_TeamworkAndCollaboration_Description");

            migrationBuilder.RenameColumn(
                name: "SC_TechnicalCompetenceEnglish",
                table: "EvaluationCriterias",
                newName: "SC_TeamworkAndCollaborationEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_TechnicalCompetence",
                table: "EvaluationCriterias",
                newName: "SC_TeamworkAndCollaboration");

            migrationBuilder.RenameColumn(
                name: "SC_PunctualityAdherenceSchedule_Value",
                table: "EvaluationCriterias",
                newName: "SC_AttitudeAndProactivity_Value");

            migrationBuilder.RenameColumn(
                name: "SC_PunctualityAdherenceSchedule_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "SC_AttitudeAndProactivity_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_PunctualityAdherenceSchedule_Description",
                table: "EvaluationCriterias",
                newName: "SC_AttitudeAndProactivity_Description");

            migrationBuilder.RenameColumn(
                name: "SC_PunctualityAdherenceScheduleEnglish",
                table: "EvaluationCriterias",
                newName: "SC_AttitudeAndProactivityEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_PunctualityAdherenceSchedule",
                table: "EvaluationCriterias",
                newName: "SC_AttitudeAndProactivity");

            migrationBuilder.RenameColumn(
                name: "SC_DeliveryProjectsAndTasks_Value",
                table: "EvaluationCriterias",
                newName: "SC_AdaptabilityAndFlexibility_Value");

            migrationBuilder.RenameColumn(
                name: "SC_DeliveryProjectsAndTasks_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "SC_AdaptabilityAndFlexibility_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_DeliveryProjectsAndTasks_Description",
                table: "EvaluationCriterias",
                newName: "SC_AdaptabilityAndFlexibility_Description");

            migrationBuilder.RenameColumn(
                name: "SC_DeliveryProjectsAndTasksEnglish",
                table: "EvaluationCriterias",
                newName: "SC_AdaptabilityAndFlexibilityEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_DeliveryProjectsAndTasks",
                table: "EvaluationCriterias",
                newName: "SC_AdaptabilityAndFlexibility");

            migrationBuilder.RenameColumn(
                name: "SC_AchievementGoalsObjectives_Value",
                table: "EvaluationCriterias",
                newName: "OC_TechnicalCompetence_Value");

            migrationBuilder.RenameColumn(
                name: "SC_AchievementGoalsObjectives_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "OC_TechnicalCompetence_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_AchievementGoalsObjectives_Description",
                table: "EvaluationCriterias",
                newName: "OC_TechnicalCompetence_Description");

            migrationBuilder.RenameColumn(
                name: "SC_AchievementGoalsObjectivesEnglish",
                table: "EvaluationCriterias",
                newName: "OC_TechnicalCompetenceEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_AchievementGoalsObjectives",
                table: "EvaluationCriterias",
                newName: "OC_TechnicalCompetence");

            migrationBuilder.RenameColumn(
                name: "OC_TeamworkAndCollaboration_Value",
                table: "EvaluationCriterias",
                newName: "OC_PunctualityAdherenceSchedule_Value");

            migrationBuilder.RenameColumn(
                name: "OC_TeamworkAndCollaboration_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "OC_PunctualityAdherenceSchedule_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_TeamworkAndCollaboration_Description",
                table: "EvaluationCriterias",
                newName: "OC_PunctualityAdherenceSchedule_Description");

            migrationBuilder.RenameColumn(
                name: "OC_TeamworkAndCollaborationEnglish",
                table: "EvaluationCriterias",
                newName: "OC_PunctualityAdherenceScheduleEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_TeamworkAndCollaboration",
                table: "EvaluationCriterias",
                newName: "OC_PunctualityAdherenceSchedule");

            migrationBuilder.RenameColumn(
                name: "OC_AttitudeAndProactivity_Value",
                table: "EvaluationCriterias",
                newName: "OC_DeliveryProjectsAndTasks_Value");

            migrationBuilder.RenameColumn(
                name: "OC_AttitudeAndProactivity_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "OC_DeliveryProjectsAndTasks_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_AttitudeAndProactivity_Description",
                table: "EvaluationCriterias",
                newName: "OC_DeliveryProjectsAndTasks_Description");

            migrationBuilder.RenameColumn(
                name: "OC_AttitudeAndProactivityEnglish",
                table: "EvaluationCriterias",
                newName: "OC_DeliveryProjectsAndTasksEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_AttitudeAndProactivity",
                table: "EvaluationCriterias",
                newName: "OC_DeliveryProjectsAndTasks");

            migrationBuilder.RenameColumn(
                name: "OC_AdaptabilityAndFlexibility_Value",
                table: "EvaluationCriterias",
                newName: "OC_AchievementGoalsObjectives_Value");

            migrationBuilder.RenameColumn(
                name: "OC_AdaptabilityAndFlexibility_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "OC_AchievementGoalsObjectives_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_AdaptabilityAndFlexibility_Description",
                table: "EvaluationCriterias",
                newName: "OC_AchievementGoalsObjectives_Description");

            migrationBuilder.RenameColumn(
                name: "OC_AdaptabilityAndFlexibilityEnglish",
                table: "EvaluationCriterias",
                newName: "OC_AchievementGoalsObjectivesEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_AdaptabilityAndFlexibility",
                table: "EvaluationCriterias",
                newName: "OC_AchievementGoalsObjectives");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SC_TeamworkAndCollaboration_Value",
                table: "EvaluationCriterias",
                newName: "SC_TechnicalCompetence_Value");

            migrationBuilder.RenameColumn(
                name: "SC_TeamworkAndCollaboration_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "SC_TechnicalCompetence_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_TeamworkAndCollaboration_Description",
                table: "EvaluationCriterias",
                newName: "SC_TechnicalCompetence_Description");

            migrationBuilder.RenameColumn(
                name: "SC_TeamworkAndCollaborationEnglish",
                table: "EvaluationCriterias",
                newName: "SC_TechnicalCompetenceEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_TeamworkAndCollaboration",
                table: "EvaluationCriterias",
                newName: "SC_TechnicalCompetence");

            migrationBuilder.RenameColumn(
                name: "SC_AttitudeAndProactivity_Value",
                table: "EvaluationCriterias",
                newName: "SC_PunctualityAdherenceSchedule_Value");

            migrationBuilder.RenameColumn(
                name: "SC_AttitudeAndProactivity_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "SC_PunctualityAdherenceSchedule_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_AttitudeAndProactivity_Description",
                table: "EvaluationCriterias",
                newName: "SC_PunctualityAdherenceSchedule_Description");

            migrationBuilder.RenameColumn(
                name: "SC_AttitudeAndProactivityEnglish",
                table: "EvaluationCriterias",
                newName: "SC_PunctualityAdherenceScheduleEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_AttitudeAndProactivity",
                table: "EvaluationCriterias",
                newName: "SC_PunctualityAdherenceSchedule");

            migrationBuilder.RenameColumn(
                name: "SC_AdaptabilityAndFlexibility_Value",
                table: "EvaluationCriterias",
                newName: "SC_DeliveryProjectsAndTasks_Value");

            migrationBuilder.RenameColumn(
                name: "SC_AdaptabilityAndFlexibility_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "SC_DeliveryProjectsAndTasks_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_AdaptabilityAndFlexibility_Description",
                table: "EvaluationCriterias",
                newName: "SC_DeliveryProjectsAndTasks_Description");

            migrationBuilder.RenameColumn(
                name: "SC_AdaptabilityAndFlexibilityEnglish",
                table: "EvaluationCriterias",
                newName: "SC_DeliveryProjectsAndTasksEnglish");

            migrationBuilder.RenameColumn(
                name: "SC_AdaptabilityAndFlexibility",
                table: "EvaluationCriterias",
                newName: "SC_DeliveryProjectsAndTasks");

            migrationBuilder.RenameColumn(
                name: "OC_TechnicalCompetence_Value",
                table: "EvaluationCriterias",
                newName: "SC_AchievementGoalsObjectives_Value");

            migrationBuilder.RenameColumn(
                name: "OC_TechnicalCompetence_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "SC_AchievementGoalsObjectives_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_TechnicalCompetence_Description",
                table: "EvaluationCriterias",
                newName: "SC_AchievementGoalsObjectives_Description");

            migrationBuilder.RenameColumn(
                name: "OC_TechnicalCompetenceEnglish",
                table: "EvaluationCriterias",
                newName: "SC_AchievementGoalsObjectivesEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_TechnicalCompetence",
                table: "EvaluationCriterias",
                newName: "SC_AchievementGoalsObjectives");

            migrationBuilder.RenameColumn(
                name: "OC_PunctualityAdherenceSchedule_Value",
                table: "EvaluationCriterias",
                newName: "OC_TeamworkAndCollaboration_Value");

            migrationBuilder.RenameColumn(
                name: "OC_PunctualityAdherenceSchedule_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "OC_TeamworkAndCollaboration_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_PunctualityAdherenceSchedule_Description",
                table: "EvaluationCriterias",
                newName: "OC_TeamworkAndCollaboration_Description");

            migrationBuilder.RenameColumn(
                name: "OC_PunctualityAdherenceScheduleEnglish",
                table: "EvaluationCriterias",
                newName: "OC_TeamworkAndCollaborationEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_PunctualityAdherenceSchedule",
                table: "EvaluationCriterias",
                newName: "OC_TeamworkAndCollaboration");

            migrationBuilder.RenameColumn(
                name: "OC_DeliveryProjectsAndTasks_Value",
                table: "EvaluationCriterias",
                newName: "OC_AttitudeAndProactivity_Value");

            migrationBuilder.RenameColumn(
                name: "OC_DeliveryProjectsAndTasks_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "OC_AttitudeAndProactivity_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_DeliveryProjectsAndTasks_Description",
                table: "EvaluationCriterias",
                newName: "OC_AttitudeAndProactivity_Description");

            migrationBuilder.RenameColumn(
                name: "OC_DeliveryProjectsAndTasksEnglish",
                table: "EvaluationCriterias",
                newName: "OC_AttitudeAndProactivityEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_DeliveryProjectsAndTasks",
                table: "EvaluationCriterias",
                newName: "OC_AttitudeAndProactivity");

            migrationBuilder.RenameColumn(
                name: "OC_AchievementGoalsObjectives_Value",
                table: "EvaluationCriterias",
                newName: "OC_AdaptabilityAndFlexibility_Value");

            migrationBuilder.RenameColumn(
                name: "OC_AchievementGoalsObjectives_DescriptionEnglish",
                table: "EvaluationCriterias",
                newName: "OC_AdaptabilityAndFlexibility_DescriptionEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_AchievementGoalsObjectives_Description",
                table: "EvaluationCriterias",
                newName: "OC_AdaptabilityAndFlexibility_Description");

            migrationBuilder.RenameColumn(
                name: "OC_AchievementGoalsObjectivesEnglish",
                table: "EvaluationCriterias",
                newName: "OC_AdaptabilityAndFlexibilityEnglish");

            migrationBuilder.RenameColumn(
                name: "OC_AchievementGoalsObjectives",
                table: "EvaluationCriterias",
                newName: "OC_AdaptabilityAndFlexibility");
        }
    }
}
