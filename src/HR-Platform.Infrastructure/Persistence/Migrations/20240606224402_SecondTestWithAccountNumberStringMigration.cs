using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SecondTestWithAccountNumberStringMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumberBinary",
                table: "BankAccounts");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumberString",
                table: "BankAccounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumberString",
                table: "BankAccounts");

            migrationBuilder.AddColumn<byte[]>(
                name: "AccountNumberBinary",
                table: "BankAccounts",
                type: "bytea",
                maxLength: 16,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
