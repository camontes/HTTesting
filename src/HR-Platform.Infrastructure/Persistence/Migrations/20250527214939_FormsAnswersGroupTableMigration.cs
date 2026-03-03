using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FormsAnswersGroupTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FormAnswerGroupId",
                table: "FormAnswers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FormAnswerGroupId1",
                table: "FormAnswers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FormAnswerGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormQuestionsTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormAnswerGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormAnswerGroup_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswers_FormAnswerGroupId1",
                table: "FormAnswers",
                column: "FormAnswerGroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswerGroup_CollaboratorId",
                table: "FormAnswerGroup",
                column: "CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_FormAnswerGroup_FormAnswerGroupId1",
                table: "FormAnswers",
                column: "FormAnswerGroupId1",
                principalTable: "FormAnswerGroup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_FormAnswerGroup_FormAnswerGroupId1",
                table: "FormAnswers");

            migrationBuilder.DropTable(
                name: "FormAnswerGroup");

            migrationBuilder.DropIndex(
                name: "IX_FormAnswers_FormAnswerGroupId1",
                table: "FormAnswers");

            migrationBuilder.DropColumn(
                name: "FormAnswerGroupId",
                table: "FormAnswers");

            migrationBuilder.DropColumn(
                name: "FormAnswerGroupId1",
                table: "FormAnswers");
        }
    }
}
