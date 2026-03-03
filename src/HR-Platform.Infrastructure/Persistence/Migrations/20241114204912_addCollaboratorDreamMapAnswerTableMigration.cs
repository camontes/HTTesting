using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addCollaboratorDreamMapAnswerTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DreamMapAnswers_Collaborators_CollaboratorId",
                table: "DreamMapAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_DreamMapAnswers_DreamMapQuestions_DreamMapQuestionId",
                table: "DreamMapAnswers");

            migrationBuilder.DropColumn(
                name: "TemplateIndicator",
                table: "DreamMapAnswers");

            migrationBuilder.RenameColumn(
                name: "CollaboratorId",
                table: "DreamMapAnswers",
                newName: "CollaboratorDreamMapAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_DreamMapAnswers_CollaboratorId",
                table: "DreamMapAnswers",
                newName: "IX_DreamMapAnswers_CollaboratorDreamMapAnswerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "DreamMapQuestionId",
                table: "DreamMapAnswers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "DreamMapAnswers",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CollaboratorDreamMapAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateIndicator = table.Column<int>(type: "integer", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorDreamMapAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorDreamMapAnswers_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorDreamMapAnswers_CollaboratorId",
                table: "CollaboratorDreamMapAnswers",
                column: "CollaboratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DreamMapAnswers_CollaboratorDreamMapAnswers_CollaboratorDre~",
                table: "DreamMapAnswers",
                column: "CollaboratorDreamMapAnswerId",
                principalTable: "CollaboratorDreamMapAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DreamMapAnswers_DreamMapQuestions_DreamMapQuestionId",
                table: "DreamMapAnswers",
                column: "DreamMapQuestionId",
                principalTable: "DreamMapQuestions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DreamMapAnswers_CollaboratorDreamMapAnswers_CollaboratorDre~",
                table: "DreamMapAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_DreamMapAnswers_DreamMapQuestions_DreamMapQuestionId",
                table: "DreamMapAnswers");

            migrationBuilder.DropTable(
                name: "CollaboratorDreamMapAnswers");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "DreamMapAnswers");

            migrationBuilder.RenameColumn(
                name: "CollaboratorDreamMapAnswerId",
                table: "DreamMapAnswers",
                newName: "CollaboratorId");

            migrationBuilder.RenameIndex(
                name: "IX_DreamMapAnswers_CollaboratorDreamMapAnswerId",
                table: "DreamMapAnswers",
                newName: "IX_DreamMapAnswers_CollaboratorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "DreamMapQuestionId",
                table: "DreamMapAnswers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TemplateIndicator",
                table: "DreamMapAnswers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DreamMapAnswers_Collaborators_CollaboratorId",
                table: "DreamMapAnswers",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DreamMapAnswers_DreamMapQuestions_DreamMapQuestionId",
                table: "DreamMapAnswers",
                column: "DreamMapQuestionId",
                principalTable: "DreamMapQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
