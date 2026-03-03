using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addDeleteFieldsCollaboratorGeneralInductionTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "CollaboratorGeneralInductions");

            migrationBuilder.DropColumn(
                name: "InductionName",
                table: "CollaboratorGeneralInductions");

            migrationBuilder.DropColumn(
                name: "IsInductionCompleted",
                table: "CollaboratorGeneralInductions");

            migrationBuilder.RenameColumn(
                name: "IsInductionDeleted",
                table: "CollaboratorGeneralInductions",
                newName: "HasInductionBeenDeletedWhenHasCompleted");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "Induction",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmailWhoDeletedByTH",
                table: "Induction",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsInductionDeleted",
                table: "Induction",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "Induction");

            migrationBuilder.DropColumn(
                name: "EmailWhoDeletedByTH",
                table: "Induction");

            migrationBuilder.DropColumn(
                name: "IsInductionDeleted",
                table: "Induction");

            migrationBuilder.RenameColumn(
                name: "HasInductionBeenDeletedWhenHasCompleted",
                table: "CollaboratorGeneralInductions",
                newName: "IsInductionDeleted");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "CollaboratorGeneralInductions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InductionName",
                table: "CollaboratorGeneralInductions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsInductionCompleted",
                table: "CollaboratorGeneralInductions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
