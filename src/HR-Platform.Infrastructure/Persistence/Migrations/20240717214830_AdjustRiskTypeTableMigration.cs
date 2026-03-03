using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustRiskTypeTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risks_Companies_CompanyId",
                table: "Risks");

            migrationBuilder.DropForeignKey(
                name: "FK_Risks_RiskTypes_RiskTypeId",
                table: "Risks");

            migrationBuilder.DropTable(
                name: "RiskTypes");

            migrationBuilder.DropIndex(
                name: "IX_Risks_RiskTypeId",
                table: "Risks");

            migrationBuilder.DropColumn(
                name: "RiskTypeId",
                table: "Risks");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Risks",
                newName: "RiskTypeMainId");

            migrationBuilder.RenameIndex(
                name: "IX_Risks_CompanyId",
                table: "Risks",
                newName: "IX_Risks_RiskTypeMainId");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Risks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DefaultRiskTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultRiskTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskTypeMains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskTypeMains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskTypeMains_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DefaultRiskTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Riesgos naturales", "Natural risks" },
                    { 2, "Riesgos biologicos", "Biological risks" },
                    { 3, "Riesgo fisico", "Physical risks" },
                    { 4, "Riesgo químico", "Chemical risks" },
                    { 5, "Riesgo psicosocial", "Psychosocial risks" },
                    { 6, "Riesgo biomecánico", "Biomechanical risks" },
                    { 7, "Ninguno", "None" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskTypeMains_CompanyId",
                table: "RiskTypeMains",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_RiskTypeMains_RiskTypeMainId",
                table: "Risks",
                column: "RiskTypeMainId",
                principalTable: "RiskTypeMains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Risks_RiskTypeMains_RiskTypeMainId",
                table: "Risks");

            migrationBuilder.DropTable(
                name: "DefaultRiskTypes");

            migrationBuilder.DropTable(
                name: "RiskTypeMains");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Risks");

            migrationBuilder.RenameColumn(
                name: "RiskTypeMainId",
                table: "Risks",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Risks_RiskTypeMainId",
                table: "Risks",
                newName: "IX_Risks_CompanyId");

            migrationBuilder.AddColumn<int>(
                name: "RiskTypeId",
                table: "Risks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RiskTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameEnglish = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RiskTypes",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, "Riesgos naturales", "Natural risks" },
                    { 2, "Riesgos biologicos", "Biological risks" },
                    { 3, "Riesgo fisico", "Physical risks" },
                    { 4, "Riesgo químico", "Chemical risks" },
                    { 5, "Riesgo psicosocial", "Psychosocial risks" },
                    { 6, "Riesgo biomecánico", "Biomechanical risks" },
                    { 7, "Ninguno", "None" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Risks_RiskTypeId",
                table: "Risks",
                column: "RiskTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_Companies_CompanyId",
                table: "Risks",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Risks_RiskTypes_RiskTypeId",
                table: "Risks",
                column: "RiskTypeId",
                principalTable: "RiskTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
