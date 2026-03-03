using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addNewFieldBenefitClaimAnswerTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "BenefitClaimAnswers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmailWhoDeletedBenefitClaim",
                table: "BenefitClaimAnswers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasDeleted",
                table: "BenefitClaimAnswers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NameWhoDeletedBenefitClaim",
                table: "BenefitClaimAnswers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "BenefitClaimAnswers");

            migrationBuilder.DropColumn(
                name: "EmailWhoDeletedBenefitClaim",
                table: "BenefitClaimAnswers");

            migrationBuilder.DropColumn(
                name: "HasDeleted",
                table: "BenefitClaimAnswers");

            migrationBuilder.DropColumn(
                name: "NameWhoDeletedBenefitClaim",
                table: "BenefitClaimAnswers");
        }
    }
}
