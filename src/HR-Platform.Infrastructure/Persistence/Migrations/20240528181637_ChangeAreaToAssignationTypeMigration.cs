using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAreaToAssignationTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyAreas_DefaultStudyAreaId",
                table: "CollaboratorEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Areas_AreaId",
                table: "Collaborators");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "DefaultStudyAreas");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Collaborators",
                newName: "AssignationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Collaborators_AreaId",
                table: "Collaborators",
                newName: "IX_Collaborators_AssignationTypeId");

            migrationBuilder.RenameColumn(
                name: "DefaultStudyAreaId",
                table: "CollaboratorEducations",
                newName: "DefaultStudyAssignationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorEducations_DefaultStudyAreaId",
                table: "CollaboratorEducations",
                newName: "IX_CollaboratorEducations_DefaultStudyAssignationTypeId");

            migrationBuilder.CreateTable(
                name: "AssignationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignationTypes", x => x.Id);
                });

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
                table: "AssignationTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Personal interno", "Internal staff" },
                    { 2, "Personal externo", "External staff" }
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

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_AssignationTypes_AssignationTypeId",
                table: "Collaborators",
                column: "AssignationTypeId",
                principalTable: "AssignationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorEducations_DefaultStudyAssignationTypes_Default~",
                table: "CollaboratorEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_AssignationTypes_AssignationTypeId",
                table: "Collaborators");

            migrationBuilder.DropTable(
                name: "AssignationTypes");

            migrationBuilder.DropTable(
                name: "DefaultStudyAssignationTypes");

            migrationBuilder.RenameColumn(
                name: "AssignationTypeId",
                table: "Collaborators",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Collaborators_AssignationTypeId",
                table: "Collaborators",
                newName: "IX_Collaborators_AreaId");

            migrationBuilder.RenameColumn(
                name: "DefaultStudyAssignationTypeId",
                table: "CollaboratorEducations",
                newName: "DefaultStudyAreaId");

            migrationBuilder.RenameIndex(
                name: "IX_CollaboratorEducations_DefaultStudyAssignationTypeId",
                table: "CollaboratorEducations",
                newName: "IX_CollaboratorEducations_DefaultStudyAreaId");

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

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
                table: "Areas",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Personal interno", "Internal staff" },
                    { 2, "Personal externo", "External staff" }
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

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Areas_AreaId",
                table: "Collaborators",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
