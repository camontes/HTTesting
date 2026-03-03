using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SurveyQuestionTypeSeedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyMandatoryType");

            migrationBuilder.CreateTable(
                name: "SurveyMandatoryTypes",
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
                    table.PrimaryKey("PK_SurveyMandatoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestionTypes",
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
                    table.PrimaryKey("PK_SurveyQuestionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    SurveyTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    SurveyMandatoryTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EmailWhoChangedByTH = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameWhoChangedByTH = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surveys_Areas_SurveyTypeId",
                        column: x => x.SurveyTypeId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surveys_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surveys_SurveyMandatoryTypes_SurveyMandatoryTypeId",
                        column: x => x.SurveyMandatoryTypeId,
                        principalTable: "SurveyMandatoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SurveyMandatoryTypes",
                columns: new[] { "Id", "IsDeleteable", "IsEditable", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, false, false, "Requerido", "Required" },
                    { 2, false, false, "Opcional", "Optional" }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestionTypes",
                columns: new[] { "Id", "IsDeleteable", "IsEditable", "Name", "NameEnglish" },
                values: new object[] { 1, false, false, "Texto corto", "Short text" });

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_CompanyId",
                table: "Surveys",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_SurveyMandatoryTypeId",
                table: "Surveys",
                column: "SurveyMandatoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_SurveyTypeId",
                table: "Surveys",
                column: "SurveyTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyQuestionTypes");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "SurveyMandatoryTypes");

            migrationBuilder.CreateTable(
                name: "SurveyMandatoryType",
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
                    table.PrimaryKey("PK_SurveyMandatoryType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SurveyMandatoryType",
                columns: new[] { "Id", "IsDeleteable", "IsEditable", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, false, false, "Requerido", "Required" },
                    { 2, false, false, "Opcional", "Optional" }
                });
        }
    }
}
