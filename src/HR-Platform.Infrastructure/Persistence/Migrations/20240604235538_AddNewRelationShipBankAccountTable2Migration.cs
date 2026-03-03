using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRelationShipBankAccountTable2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_BankAccounts_BankAccountId1",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_BankAccountId1",
                table: "Collaborators");

            migrationBuilder.DropColumn(
                name: "BankAccountId1",
                table: "Collaborators");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_BankAccountId",
                table: "Collaborators",
                column: "BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_BankAccounts_BankAccountId",
                table: "Collaborators",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_BankAccounts_BankAccountId",
                table: "Collaborators");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_BankAccountId",
                table: "Collaborators");

            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountId1",
                table: "Collaborators",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_BankAccountId1",
                table: "Collaborators",
                column: "BankAccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_BankAccounts_BankAccountId1",
                table: "Collaborators",
                column: "BankAccountId1",
                principalTable: "BankAccounts",
                principalColumn: "Id");
        }
    }
}
