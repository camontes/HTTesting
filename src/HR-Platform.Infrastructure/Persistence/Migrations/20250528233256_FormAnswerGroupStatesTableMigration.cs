using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FormAnswerGroupStatesTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswerGroup_Collaborators_CollaboratorId",
                table: "FormAnswerGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswerGroup_Forms_FormId",
                table: "FormAnswerGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_FormAnswerGroup_FormAnswerGroupId",
                table: "FormAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormAnswerGroup",
                table: "FormAnswerGroup");

            migrationBuilder.RenameTable(
                name: "FormAnswerGroup",
                newName: "FormAnswerGroups");

            migrationBuilder.RenameIndex(
                name: "IX_FormAnswerGroup_FormId",
                table: "FormAnswerGroups",
                newName: "IX_FormAnswerGroups_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_FormAnswerGroup_CollaboratorId",
                table: "FormAnswerGroups",
                newName: "IX_FormAnswerGroups_CollaboratorId");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionState",
                table: "FormAnswerGroups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "File",
                table: "FormAnswerGroups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "FormAnswerGroups",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormAnswerGroupStateId",
                table: "FormAnswerGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormAnswerGroups",
                table: "FormAnswerGroups",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FormAnswerGroupStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormAnswerGroupStates", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "FormAnswerGroupStates",
                columns: new[] { "Id", "IsDeleteable", "IsEditable", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, false, false, "Ninguno", "None" },
                    { 2, false, false, "Aprobado", "Approved" },
                    { 3, false, false, "Negado", "Denied" },
                    { 4, false, false, "Pendiente", "Pending" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswerGroups_Collaborators_CollaboratorId",
                table: "FormAnswerGroups",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswerGroups_Forms_FormId",
                table: "FormAnswerGroups",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_FormAnswerGroups_FormAnswerGroupId",
                table: "FormAnswers",
                column: "FormAnswerGroupId",
                principalTable: "FormAnswerGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswerGroups_Collaborators_CollaboratorId",
                table: "FormAnswerGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswerGroups_Forms_FormId",
                table: "FormAnswerGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_FormAnswerGroups_FormAnswerGroupId",
                table: "FormAnswers");

            migrationBuilder.DropTable(
                name: "FormAnswerGroupStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormAnswerGroups",
                table: "FormAnswerGroups");

            migrationBuilder.DropColumn(
                name: "DescriptionState",
                table: "FormAnswerGroups");

            migrationBuilder.DropColumn(
                name: "File",
                table: "FormAnswerGroups");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "FormAnswerGroups");

            migrationBuilder.DropColumn(
                name: "FormAnswerGroupStateId",
                table: "FormAnswerGroups");

            migrationBuilder.RenameTable(
                name: "FormAnswerGroups",
                newName: "FormAnswerGroup");

            migrationBuilder.RenameIndex(
                name: "IX_FormAnswerGroups_FormId",
                table: "FormAnswerGroup",
                newName: "IX_FormAnswerGroup_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_FormAnswerGroups_CollaboratorId",
                table: "FormAnswerGroup",
                newName: "IX_FormAnswerGroup_CollaboratorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormAnswerGroup",
                table: "FormAnswerGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswerGroup_Collaborators_CollaboratorId",
                table: "FormAnswerGroup",
                column: "CollaboratorId",
                principalTable: "Collaborators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswerGroup_Forms_FormId",
                table: "FormAnswerGroup",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_FormAnswerGroup_FormAnswerGroupId",
                table: "FormAnswers",
                column: "FormAnswerGroupId",
                principalTable: "FormAnswerGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
