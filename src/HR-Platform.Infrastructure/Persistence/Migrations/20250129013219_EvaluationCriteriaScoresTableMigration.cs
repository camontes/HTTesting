using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EvaluationCriteriaScoresTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationCriteriaScore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    EvaluationCriteriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameEnglish = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DescriptionEnglish = table.Column<string>(type: "text", nullable: false),
                    LowerScore = table.Column<int>(type: "integer", nullable: false),
                    UpperScore = table.Column<int>(type: "integer", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCriteriaScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationCriteriaScore_EvaluationCriterias_EvaluationCrite~",
                        column: x => x.EvaluationCriteriaId,
                        principalTable: "EvaluationCriterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationCriteriaScore_EvaluationCriteriaId",
                table: "EvaluationCriteriaScore",
                column: "EvaluationCriteriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationCriteriaScore");
        }
    }
}
