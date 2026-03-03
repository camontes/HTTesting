using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SurveyMandatoryTypeSeedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "SurveyMandatoryType");

            migrationBuilder.DropColumn(
                name: "EditionDate",
                table: "SurveyMandatoryType");

            migrationBuilder.InsertData(
                table: "SurveyMandatoryType",
                columns: new[] { "Id", "IsDeleteable", "IsEditable", "Name", "NameEnglish" },
                values: new object[,]
                {
                    { 1, false, false, "Requerido", "Required" },
                    { 2, false, false, "Opcional", "Optional" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SurveyMandatoryType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SurveyMandatoryType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "SurveyMandatoryType",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EditionDate",
                table: "SurveyMandatoryType",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
