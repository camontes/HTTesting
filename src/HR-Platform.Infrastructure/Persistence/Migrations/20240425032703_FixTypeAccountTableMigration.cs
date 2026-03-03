using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixTypeAccountTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_TypeAccounts_TypeAccountId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeAccountId",
                table: "Collaborators",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_TypeAccounts_TypeAccountId",
                table: "Collaborators",
                column: "TypeAccountId",
                principalTable: "TypeAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_TypeAccounts_TypeAccountId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeAccountId",
                table: "Collaborators",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_TypeAccounts_TypeAccountId",
                table: "Collaborators",
                column: "TypeAccountId",
                principalTable: "TypeAccounts",
                principalColumn: "Id");
        }
    }
}
