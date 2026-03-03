using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldCollaboratorCriteriaAnswerTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerByCriteria",
                table: "CollaboratorCriteriaAnswer");

            migrationBuilder.AddColumn<int>(
                name: "CriteriaScorePercentage",
                table: "CollaboratorCriteriaAnswer",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriteriaScorePercentage",
                table: "CollaboratorCriteriaAnswer");

            migrationBuilder.AddColumn<string>(
                name: "AnswerByCriteria",
                table: "CollaboratorCriteriaAnswer",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }
    }
}
