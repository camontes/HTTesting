using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixStudyAreaTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyAssignationTypes_Default~",
                table: "CollaboratorEducations");

            migrationBuilder.DropTable(
                name: "DefaultStudyAssignationTypes");

            migrationBuilder.RenameColumn(
                name: "DefaultStudyAssignationTypeId",
                table: "CollaboratorEducations",
                newName: "DefaultStudyAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorEducations_DefaultStudyAssignationTypeId",
                table: "CollaboratorEducations",
                newName: "IX_CollaboratorEducations_DefaultStudyAreaId");

            migrationBuilder.CreateTable(
                name: "DefaultStudyAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultStudyAreas", x => x.Id);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyAreas_DefaultStudyAreaId",
                table: "CollaboratorEducations",
                column: "DefaultStudyAreaId",
                principalTable: "DefaultStudyAreas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyAreas_DefaultStudyAreaId",
                table: "CollaboratorEducations");

            migrationBuilder.DropTable(
                name: "DefaultStudyAreas");

            migrationBuilder.RenameColumn(
                name: "DefaultStudyAreaId",
                table: "CollaboratorEducations",
                newName: "DefaultStudyAssignationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorEducations_DefaultStudyAreaId",
                table: "CollaboratorEducations",
                newName: "IX_CollaboratorEducations_DefaultStudyAssignationTypeId");

            migrationBuilder.CreateTable(
                name: "DefaultStudyAssignationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultStudyAssignationTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultStudyAssignationTypes",
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

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyAssignationTypes_Default~",
                table: "CollaboratorEducations",
                column: "DefaultStudyAssignationTypeId",
                principalTable: "DefaultStudyAssignationTypes",
                principalColumn: "Id");
        }
    }
}
