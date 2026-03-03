using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustOrganizationChartTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrganizationCharts");

            migrationBuilder.DropColumn(
                name: "NameEnglish",
                table: "OrganizationCharts");

            migrationBuilder.DropColumn(
                name: "VideoCreatedDate",
                table: "OrganizationCharts");

            migrationBuilder.DropColumn(
                name: "VideoName",
                table: "OrganizationCharts");

            migrationBuilder.DropColumn(
                name: "VideoURL",
                table: "OrganizationCharts");

            migrationBuilder.RenameColumn(
                name: "IsOnlyVideoURL",
                table: "OrganizationCharts",
                newName: "IsByUrl");

            migrationBuilder.AddColumn<bool>(
                name: "IsByFile",
                table: "OrganizationCharts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsByFile",
                table: "OrganizationCharts");

            migrationBuilder.RenameColumn(
                name: "IsByUrl",
                table: "OrganizationCharts",
                newName: "IsOnlyVideoURL");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrganizationCharts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEnglish",
                table: "OrganizationCharts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "VideoCreatedDate",
                table: "OrganizationCharts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "VideoName",
                table: "OrganizationCharts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoURL",
                table: "OrganizationCharts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
