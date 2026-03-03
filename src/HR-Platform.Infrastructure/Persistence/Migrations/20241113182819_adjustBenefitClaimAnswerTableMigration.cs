using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class adjustBenefitClaimAnswerTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnotherContraint",
                table: "BenefitClaimAnswers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailWhoManagedClaim",
                table: "BenefitClaimAnswers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAnotherContraint",
                table: "BenefitClaimAnswers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailableForAll",
                table: "BenefitClaimAnswers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MinimumMonthsConstraint",
                table: "BenefitClaimAnswers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NameWhoManagedClaim",
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
                name: "AnotherContraint",
                table: "BenefitClaimAnswers");

            migrationBuilder.DropColumn(
                name: "EmailWhoManagedClaim",
                table: "BenefitClaimAnswers");

            migrationBuilder.DropColumn(
                name: "IsAnotherContraint",
                table: "BenefitClaimAnswers");

            migrationBuilder.DropColumn(
                name: "IsAvailableForAll",
                table: "BenefitClaimAnswers");

            migrationBuilder.DropColumn(
                name: "MinimumMonthsConstraint",
                table: "BenefitClaimAnswers");

            migrationBuilder.DropColumn(
                name: "NameWhoManagedClaim",
                table: "BenefitClaimAnswers");
        }
    }
}
