using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCollaboratorCriteriaTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaboratorCriteria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorEvaluatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    EvaluatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorCriteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorCriteria_Collaborators_CollaboratorEvaluatedId",
                        column: x => x.CollaboratorEvaluatedId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorCriteria_Collaborators_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorCriteria_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorCriteriaAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    EvaluationCriteriaTypeId = table.Column<int>(type: "integer", nullable: false),
                    GeneralObjetiveCriteriaPercentage = table.Column<int>(type: "integer", nullable: false),
                    GeneralSubjetiveCriteriaPercentage = table.Column<int>(type: "integer", nullable: false),
                    CollaboratorCriteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CriteriaName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CriteriaNameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CriteriaPercentage = table.Column<int>(type: "integer", nullable: false),
                    CriteriaScoreName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CriteriaScoreNameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AnswerByCriteria = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    PositionEnglish = table.Column<string>(type: "text", nullable: false),
                    Comments = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorCriteriaAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorCriteriaAnswer_CollaboratorCriteria_Collaborato~",
                        column: x => x.CollaboratorCriteriaId,
                        principalTable: "CollaboratorCriteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorCriteriaAnswer_EvaluationCriteriaType_Evaluatio~",
                        column: x => x.EvaluationCriteriaTypeId,
                        principalTable: "EvaluationCriteriaType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorCriteria_CollaboratorEvaluatedId",
                table: "CollaboratorCriteria",
                column: "CollaboratorEvaluatedId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorCriteria_EvaluatorId",
                table: "CollaboratorCriteria",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorCriteria_PositionId",
                table: "CollaboratorCriteria",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorCriteriaAnswer_CollaboratorCriteriaId",
                table: "CollaboratorCriteriaAnswer",
                column: "CollaboratorCriteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorCriteriaAnswer_EvaluationCriteriaTypeId",
                table: "CollaboratorCriteriaAnswer",
                column: "EvaluationCriteriaTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorCriteriaAnswer");

            migrationBuilder.DropTable(
                name: "CollaboratorCriteria");
        }
    }
}
