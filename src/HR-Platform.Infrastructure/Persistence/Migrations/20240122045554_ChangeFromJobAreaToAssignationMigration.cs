using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFromJobAreaToAssignationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultJobAreas");

            migrationBuilder.DropTable(
                name: "JobAreas");

            migrationBuilder.CreateTable(
                name: "Assignations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    IsInternalAssignation = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefaultAssignations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultAssignations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DefaultAssignations",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Personal interno", "Internal staff" },
                    { 2, "Personal externo", "External staff" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignations_CompanyId",
                table: "Assignations",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignations");

            migrationBuilder.DropTable(
                name: "DefaultAssignations");

            migrationBuilder.CreateTable(
                name: "DefaultJobAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultJobAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobAreas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleteable = table.Column<bool>(type: "boolean", nullable: false),
                    IsEditable = table.Column<bool>(type: "boolean", nullable: false),
                    IsInternalAssignation = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobAreas_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DefaultJobAreas",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Personal interno", "Internal staff" },
                    { 2, "Personal externo", "External staff" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobAreas_CompanyId",
                table: "JobAreas",
                column: "CompanyId");
        }
    }
}
