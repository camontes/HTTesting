using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTechnologiesTablesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultKnowledgeLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultKnowledgeLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DefaultTechnologyNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultTechnologyNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollaboratorTechnologyTools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultTechnologyNameId = table.Column<int>(type: "integer", nullable: true),
                    DefaultKnowledgeLevelId = table.Column<int>(type: "integer", nullable: true),
                    OtherTechnologyName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OtherTechnologyNameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OtherKnowledgeLevelName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OtherKnowledgeLevelNameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorTechnologyTools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorTechnologyTools_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorTechnologyTools_DefaultKnowledgeLevels_DefaultK~",
                        column: x => x.DefaultKnowledgeLevelId,
                        principalTable: "DefaultKnowledgeLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CollaboratorTechnologyTools_DefaultTechnologyNames_DefaultT~",
                        column: x => x.DefaultTechnologyNameId,
                        principalTable: "DefaultTechnologyNames",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "DefaultKnowledgeLevels",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Basico", "Basic" },
                    { 3, "Intermedio", "Intermediate" },
                    { 4, "Avanzado", "Advanced" }
                });

            migrationBuilder.InsertData(
                table: "DefaultTechnologyNames",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "HTML 5", "HTML 5" },
                    { 3, "CSS3", "CSS3" },
                    { 4, "Javascript", "Javascript" },
                    { 5, "Angular", "Angular" },
                    { 6, "React_Redux", "React Redux" },
                    { 7, "VUE", "VUE" },
                    { 8, "Sql_Server", "Sql Server" },
                    { 9, "Mongo_Db", "Mongo Db" },
                    { 10, "Oracle", "Oracle" },
                    { 11, "Mysql", "Mysql" },
                    { 12, "IBM_Db2", "IBM Db2" },
                    { 13, "Postgresql", "Postgresql" },
                    { 14, "Redis", "Redis" },
                    { 15, "Firebase", "Firebase" },
                    { 16, ".Net", ".Net" },
                    { 17, "Node_Js", "Node Js" },
                    { 18, "As400", "As400" },
                    { 19, "Java", "Java" },
                    { 20, "PHP", "PHP" },
                    { 21, "Python", "Python" },
                    { 22, "Ruby", "Ruby" },
                    { 23, "Cobol", "Cobol" },
                    { 24, "Kotlin", "Kotlin" },
                    { 25, "Swift", "Swift" },
                    { 26, "Java2", "Java2" },
                    { 27, "Xamarin", "Xamarin" },
                    { 28, "Ionic", "Ionic" },
                    { 29, "React Native", "React Native" },
                    { 30, "Microsoft Azure", "Microsoft Azure" },
                    { 31, "Google Cloud Plattform", "Google Cloud Plattform" },
                    { 32, "IBM Bluemix", "IBM Bluemix" },
                    { 33, "Heroku", "Heroku" },
                    { 34, "Firebase2", "Firebase2" },
                    { 35, "Firebase3", "Firebase3" },
                    { 36, "Selenium", "Selenium" },
                    { 37, "Appium", "Appium" },
                    { 38, "Nunit", "Nunit" },
                    { 39, "Junit", "Junit" },
                    { 40, "Jmeter", "Jmeter" },
                    { 41, "Testing_Funcional", "Testing Funcional" },
                    { 42, "Azure_Devops", "Azure Devops" },
                    { 43, "Gitlab", "Gitlab" },
                    { 44, "Jenkins", "Jenkins" },
                    { 45, "Git", "Git" },
                    { 46, "TFS", "TFS" },
                    { 47, "SVN", "SVN" },
                    { 48, "RPA", "RPA" },
                    { 49, "Apache", "Apache" },
                    { 50, "IIS", "IIS" },
                    { 51, "Tomcat", "Tomcat" },
                    { 52, "Blockchain", "Blockchain" },
                    { 53, "Machine_Learning", "Machine Learning" },
                    { 54, "Wso2", "Wso2" },
                    { 55, "Otras", "Others" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTechnologyTools_CollaboratorId",
                table: "CollaboratorTechnologyTools",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTechnologyTools_DefaultKnowledgeLevelId",
                table: "CollaboratorTechnologyTools",
                column: "DefaultKnowledgeLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTechnologyTools_DefaultTechnologyNameId",
                table: "CollaboratorTechnologyTools",
                column: "DefaultTechnologyNameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorTechnologyTools");

            migrationBuilder.DropTable(
                name: "DefaultKnowledgeLevels");

            migrationBuilder.DropTable(
                name: "DefaultTechnologyNames");
        }
    }
}
