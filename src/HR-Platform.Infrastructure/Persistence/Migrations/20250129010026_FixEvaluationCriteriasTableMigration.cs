using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixEvaluationCriteriasTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationCriterias_Roles_RoleId",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_AchievementGoalsObjectives",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_AchievementGoalsObjectivesEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_AchievementGoalsObjectives_Description",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_AchievementGoalsObjectives_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_AchievementGoalsObjectives_Value",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_DeliveryProjectsAndTasks",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_DeliveryProjectsAndTasksEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_DeliveryProjectsAndTasks_Description",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_DeliveryProjectsAndTasks_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_DeliveryProjectsAndTasks_Value",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_PunctualityAdherenceSchedule",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_PunctualityAdherenceScheduleEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_PunctualityAdherenceSchedule_Description",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_PunctualityAdherenceSchedule_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_PunctualityAdherenceSchedule_Value",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_TechnicalCompetence",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_TechnicalCompetenceEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_TechnicalCompetence_Description",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_TechnicalCompetence_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "OC_TechnicalCompetence_Value",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "ObjectiveCriteria",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AdaptabilityAndFlexibility",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AdaptabilityAndFlexibilityEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AdaptabilityAndFlexibility_Description",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AdaptabilityAndFlexibility_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AdaptabilityAndFlexibility_Value",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AttitudeAndProactivity",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AttitudeAndProactivityEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AttitudeAndProactivity_Description",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AttitudeAndProactivity_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_AttitudeAndProactivity_Value",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_TeamworkAndCollaboration",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_TeamworkAndCollaborationEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_TeamworkAndCollaboration_Description",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "SC_TeamworkAndCollaboration_DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.RenameColumn(
                name: "SubjectiveCriteria",
                table: "EvaluationCriterias",
                newName: "Percentage");

            migrationBuilder.RenameColumn(
                name: "SC_TeamworkAndCollaboration_Value",
                table: "EvaluationCriterias",
                newName: "EvaluationCriteriaTypeId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "EvaluationCriterias",
                newName: "PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluationCriterias_RoleId",
                table: "EvaluationCriterias",
                newName: "IX_EvaluationCriterias_PositionId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEnglish",
                table: "EvaluationCriterias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriterias_EvaluationCriteriaTypeId",
                table: "EvaluationCriterias",
                column: "EvaluationCriteriaTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationCriterias_EvaluationCriteriaType_EvaluationCriter~",
                table: "EvaluationCriterias",
                column: "EvaluationCriteriaTypeId",
                principalTable: "EvaluationCriteriaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationCriterias_Positions_PositionId",
                table: "EvaluationCriterias",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationCriterias_EvaluationCriteriaType_EvaluationCriter~",
                table: "EvaluationCriterias");

            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationCriterias_Positions_PositionId",
                table: "EvaluationCriterias");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationCriterias_EvaluationCriteriaTypeId",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "DescriptionEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EvaluationCriterias");

            migrationBuilder.DropColumn(
                name: "NameEnglish",
                table: "EvaluationCriterias");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "EvaluationCriterias",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Percentage",
                table: "EvaluationCriterias",
                newName: "SubjectiveCriteria");

            migrationBuilder.RenameColumn(
                name: "EvaluationCriteriaTypeId",
                table: "EvaluationCriterias",
                newName: "SC_TeamworkAndCollaboration_Value");

            migrationBuilder.RenameIndex(
                name: "IX_EvaluationCriterias_PositionId",
                table: "EvaluationCriterias",
                newName: "IX_EvaluationCriterias_RoleId");

            migrationBuilder.AddColumn<string>(
                name: "OC_AchievementGoalsObjectives",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_AchievementGoalsObjectivesEnglish",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_AchievementGoalsObjectives_Description",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_AchievementGoalsObjectives_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OC_AchievementGoalsObjectives_Value",
                table: "EvaluationCriterias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OC_DeliveryProjectsAndTasks",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_DeliveryProjectsAndTasksEnglish",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_DeliveryProjectsAndTasks_Description",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_DeliveryProjectsAndTasks_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OC_DeliveryProjectsAndTasks_Value",
                table: "EvaluationCriterias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OC_PunctualityAdherenceSchedule",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_PunctualityAdherenceScheduleEnglish",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_PunctualityAdherenceSchedule_Description",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_PunctualityAdherenceSchedule_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OC_PunctualityAdherenceSchedule_Value",
                table: "EvaluationCriterias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OC_TechnicalCompetence",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_TechnicalCompetenceEnglish",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_TechnicalCompetence_Description",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OC_TechnicalCompetence_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OC_TechnicalCompetence_Value",
                table: "EvaluationCriterias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ObjectiveCriteria",
                table: "EvaluationCriterias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SC_AdaptabilityAndFlexibility",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_AdaptabilityAndFlexibilityEnglish",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_AdaptabilityAndFlexibility_Description",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_AdaptabilityAndFlexibility_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SC_AdaptabilityAndFlexibility_Value",
                table: "EvaluationCriterias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SC_AttitudeAndProactivity",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_AttitudeAndProactivityEnglish",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_AttitudeAndProactivity_Description",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_AttitudeAndProactivity_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SC_AttitudeAndProactivity_Value",
                table: "EvaluationCriterias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SC_TeamworkAndCollaboration",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_TeamworkAndCollaborationEnglish",
                table: "EvaluationCriterias",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_TeamworkAndCollaboration_Description",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SC_TeamworkAndCollaboration_DescriptionEnglish",
                table: "EvaluationCriterias",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationCriterias_Roles_RoleId",
                table: "EvaluationCriterias",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
