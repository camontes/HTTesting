using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixSurveyQuestionTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_SurveyMandatoryTypes_SurveyMandatoryTypeId",
                table: "Surveys");

            migrationBuilder.DropTable(
                name: "SurveyMandatoryTypes");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_SurveyMandatoryTypeId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "SurveyMandatoryTypeId",
                table: "Surveys");

            migrationBuilder.AddColumn<int>(
                name: "SurveyQuestionMandatoryTypeId",
                table: "SurveyQuestion",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SurveyQuestionMandatoryTypes",
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
                    table.PrimaryKey("PK_SurveyQuestionMandatoryTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestionMandatoryTypes",
                columns: new[] { "Id", "IsDeleteable", "IsEditable", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, false, false, "Requerido", "Required" },
                    { 2, false, false, "Opcional", "Optional" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestion_SurveyQuestionMandatoryTypeId",
                table: "SurveyQuestion",
                column: "SurveyQuestionMandatoryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestion_SurveyQuestionMandatoryTypes_SurveyQuestionM~",
                table: "SurveyQuestion",
                column: "SurveyQuestionMandatoryTypeId",
                principalTable: "SurveyQuestionMandatoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestion_SurveyQuestionMandatoryTypes_SurveyQuestionM~",
                table: "SurveyQuestion");

            migrationBuilder.DropTable(
                name: "SurveyQuestionMandatoryTypes");

            migrationBuilder.DropIndex(
                name: "IX_SurveyQuestion_SurveyQuestionMandatoryTypeId",
                table: "SurveyQuestion");

            migrationBuilder.DropColumn(
                name: "SurveyQuestionMandatoryTypeId",
                table: "SurveyQuestion");

            migrationBuilder.AddColumn<int>(
                name: "SurveyMandatoryTypeId",
                table: "Surveys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SurveyMandatoryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyMandatoryTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SurveyMandatoryTypes",
                columns: new[] { "Id", "IsDeleteable", "IsEditable", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, false, false, "Requerido", "Required" },
                    { 2, false, false, "Opcional", "Optional" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_SurveyMandatoryTypeId",
                table: "Surveys",
                column: "SurveyMandatoryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_SurveyMandatoryTypes_SurveyMandatoryTypeId",
                table: "Surveys",
                column: "SurveyMandatoryTypeId",
                principalTable: "SurveyMandatoryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
