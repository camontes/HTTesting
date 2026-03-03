using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCollaboratorTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "Collaborators",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Collaborators",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Collaborators",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Collaborators",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EconomicLevelId",
                table: "Collaborators",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocationAddress",
                table: "Collaborators",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Collaborators",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Collaborators",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_EconomicLevelId",
                table: "Collaborators",
                column: "EconomicLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_EconomicLevels_EconomicLevelId",
                table: "Collaborators",
                column: "EconomicLevelId",
                principalTable: "EconomicLevels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_EconomicLevels_EconomicLevelId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_EconomicLevelId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "EconomicLevelId",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "LocationAddress",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Collaborators");
        }
    }
}
