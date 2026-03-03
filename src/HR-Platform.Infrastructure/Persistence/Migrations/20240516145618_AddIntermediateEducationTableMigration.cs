using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIntermediateEducationTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorLanguages_DefaultProfessions_DefaultProfessionId",
                table: "CollaboratorLanguages");

            migrationBuilder.DropIndex(
                name: "IX_CollaboratorLanguages_DefaultProfessionId",
                table: "CollaboratorLanguages");

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DefaultStudyAreas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DefaultStudyTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DefaultStudyTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DefaultStudyTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DefaultStudyTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "DefaultProfessionId",
                table: "CollaboratorLanguages");

            migrationBuilder.CreateTable(
                name: "CollaboratorEducations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollaboratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    InstitutionName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DefaultProfessionId = table.Column<int>(type: "integer", nullable: false),
                    EducationalLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    DefaultStudyTypeId = table.Column<int>(type: "integer", nullable: false),
                    IsCertificated = table.Column<bool>(type: "boolean", nullable: false),
                    DefaultStudyAreaId = table.Column<int>(type: "integer", nullable: false),
                    IsCompletedStudy = table.Column<bool>(type: "boolean", nullable: false),
                    StartEducationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndEducationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EducationFile = table.Column<string>(type: "text", nullable: true),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollaboratorEducations_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorEducations_DefaultProfessions_DefaultProfession~",
                        column: x => x.DefaultProfessionId,
                        principalTable: "DefaultProfessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorEducations_DefaultStudyAreas_DefaultStudyAreaId",
                        column: x => x.DefaultStudyAreaId,
                        principalTable: "DefaultStudyAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorEducations_DefaultStudyTypes_DefaultStudyTypeId",
                        column: x => x.DefaultStudyTypeId,
                        principalTable: "DefaultStudyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorEducations_EducationalLevels_EducationalLevelId",
                        column: x => x.EducationalLevelId,
                        principalTable: "EducationalLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEducations_CollaboratorId",
                table: "CollaboratorEducations",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEducations_DefaultProfessionId",
                table: "CollaboratorEducations",
                column: "DefaultProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEducations_DefaultStudyAreaId",
                table: "CollaboratorEducations",
                column: "DefaultStudyAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEducations_DefaultStudyTypeId",
                table: "CollaboratorEducations",
                column: "DefaultStudyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorEducations_EducationalLevelId",
                table: "CollaboratorEducations",
                column: "EducationalLevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorEducations");

            migrationBuilder.AddColumn<int>(
                name: "DefaultProfessionId",
                table: "CollaboratorLanguages",
                type: "integer",
                nullable: true);

            migrationBuilder.InsertData(
                table: "DefaultStudyAreas",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Ciencias administrativas, económicas y financieras", "Management, economic and financial sciences" },
                    { 3, "Ciencias de la salud", "Health sciences" },
                    { 4, "Ciencias sociales y humanas", "Social and human sciences" },
                    { 5, "Diseño", "Design" },
                    { 6, "Comunicación", "Communication" },
                    { 7, "Ingeniería y tecnología", "Engineering and technology" },
                    { 8, "Educación", "Education" },
                    { 9, "Derecho", "Law" },
                    { 10, "Empresarial", "Business" },
                    { 11, "Otra", "Other" }
                });

            migrationBuilder.InsertData(
                table: "DefaultStudyTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Ninguno", "None" },
                    { 2, "Educación formal", "Formal education" },
                    { 3, "Educación complementaria", "Complementary education" },
                    { 4, "Certificaciones", "Certifications" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorLanguages_DefaultProfessionId",
                table: "CollaboratorLanguages",
                column: "DefaultProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorLanguages_DefaultProfessions_DefaultProfessionId",
                table: "CollaboratorLanguages",
                column: "DefaultProfessionId",
                principalTable: "DefaultProfessions",
                principalColumn: "Id");
        }
    }
}
