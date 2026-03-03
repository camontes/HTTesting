using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEvaluationCriteriaTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationCriterias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectiveCriteria = table.Column<int>(type: "integer", nullable: false),
                    ObjectiveCriteria = table.Column<int>(type: "integer", nullable: false),
                    SC_AchievementGoalsObjectives = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SC_AchievementGoalsObjectives_Value = table.Column<int>(type: "integer", nullable: false),
                    SC_AchievementGoalsObjectives_Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SC_TechnicalCompetence = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SC_TechnicalCompetence_Value = table.Column<int>(type: "integer", nullable: false),
                    SC_TechnicalCompetence_Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SC_ObjectiveCriteria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SC_ObjectiveCriteria_Value = table.Column<int>(type: "integer", nullable: false),
                    SC_ObjectiveCriteria_Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SC_PunctualityAdherenceSchedule = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SC_PunctualityAdherenceSchedule_Value = table.Column<int>(type: "integer", nullable: false),
                    SC_PunctualityAdherenceSchedule_Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OC_TeamworkAndCollaboration = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OC_TeamworkAndCollaboration_Value = table.Column<int>(type: "integer", nullable: false),
                    OC_TeamworkAndCollaboration_Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OC_AdaptabilityAndFlexibility = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OC_AdaptabilityAndFlexibility_Value = table.Column<int>(type: "integer", nullable: false),
                    OC_AdaptabilityAndFlexibility_Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    OC_AttitudeAndProactivity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OC_AttitudeAndProactivity_Value = table.Column<int>(type: "integer", nullable: false),
                    OC_AttitudeAndProactivity_Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationCriterias_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriterias_RoleId",
                table: "EvaluationCriterias",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationCriterias");
        }
    }
}
