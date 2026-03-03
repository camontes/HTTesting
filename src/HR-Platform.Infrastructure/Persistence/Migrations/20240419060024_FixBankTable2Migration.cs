using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixBankTable2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Banks_BankId",
                table: "Collaborators");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankId",
                table: "Collaborators",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "DefaultBanks",
                columns: new[] { "Id", "Name", "NameEnglish" },
                values: new object[] { 6, "Ninguno", "None" });

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Banks_BankId",
                table: "Collaborators",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Banks_BankId",
                table: "Collaborators");

            migrationBuilder.DeleteData(
                table: "DefaultBanks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<Guid>(
                name: "BankId",
                table: "Collaborators",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Banks_BankId",
                table: "Collaborators",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id");
        }
    }
}
